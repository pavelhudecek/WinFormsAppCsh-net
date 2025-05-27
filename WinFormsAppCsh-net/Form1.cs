using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace WinFormsAppCsh_net
{
    public partial class Form1 : Form
    {
        private UdpClient _udpClient;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonUDPstart_Click(object sender, EventArgs e)
        {
            // Pokud chcete zároveò IPv4 i IPv6 pøíjem, použijte DualMode
            _udpClient = new UdpClient(AddressFamily.InterNetworkV6);
            _udpClient.Client.DualMode = true;   // umožní pøijímat i IPv4
            _udpClient.Client.Bind(new IPEndPoint(IPAddress.IPv6Any, 12345));
            
            udpBeginReceive();
            buttonUDPstart.Enabled = false;
            buttonStop.Enabled = true;
            buttonSendMsg.Enabled = true;
        }

        private void udpBeginReceive()
        {
            // zaregistrujeme callback, který se spustí pøi pøijetí paketu
            _udpClient.BeginReceive(ReceiveCallback, null);
        }
        private void ReceiveCallback(IAsyncResult ar)
        {
            IPEndPoint remoteEP = null;
            byte[] buffer;

            try
            {
                buffer = _udpClient.EndReceive(ar, ref remoteEP);
            }
            catch (ObjectDisposedException)
            {
                // Socket už je uzavøený
                return;
            }
            catch (Exception ex)
            {
                // Zpracovat chybu, pøípadnì zkusit znovu
                this.Invoke((Action)(() =>
                    listMessages.Items.Add($"Chyba: {ex.Message}")
                ));
                udpBeginReceive();
                return;
            }

            // Dekódujeme zprávu a pøidáme na UI
            string msg = Encoding.UTF8.GetString(buffer);
            this.Invoke((Action)(() =>
                listMessages.Items.Add("Recv: " + msg)
            ));

            if (!msg.StartsWith("potvrzeno:") && !msg.StartsWith("acknowledged:"))
            {
                // Odpovíme potvrzením
                string confirmation = "potvrzeno: " + remoteEP.Address.ToString();
                byte[] confirmationData = Encoding.UTF8.GetBytes(confirmation);
                _udpClient.Send(confirmationData, confirmationData.Length, remoteEP);
            }

            // Zaregistrujeme pøíjem další zprávy
            udpBeginReceive();
        }

        private void buttonTCPstart_Click(object sender, EventArgs e)
        {

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            _udpClient.Close();
            buttonUDPstart.Enabled = true;
            buttonSendMsg.Enabled = false;
        }

        private void buttonSendMsg_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(textSendMsg.Text);
                // Adresa a port pøíjemce
                IPAddress remoteIp = IPAddress.Parse(textIPadr.Text);
                int remotePort = Convert.ToInt32(textPort.Text);

                var endpoint = new IPEndPoint(remoteIp, remotePort);
                _udpClient.Send(data, data.Length, endpoint);
            }
            catch (FormatException)
            {
                MessageBox.Show("Neplatná IP adresa.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Odeslání selhalo: {ex.Message}");
            }
        }
    }
}
