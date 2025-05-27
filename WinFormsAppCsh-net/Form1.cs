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
            // Pokud chcete z�rove� IPv4 i IPv6 p��jem, pou�ijte DualMode
            _udpClient = new UdpClient(AddressFamily.InterNetworkV6);
            _udpClient.Client.DualMode = true;   // umo�n� p�ij�mat i IPv4
            _udpClient.Client.Bind(new IPEndPoint(IPAddress.IPv6Any, 12345));
            
            udpBeginReceive();
            buttonUDPstart.Enabled = false;
            buttonStop.Enabled = true;
            buttonSendMsg.Enabled = true;
        }

        private void udpBeginReceive()
        {
            // zaregistrujeme callback, kter� se spust� p�i p�ijet� paketu
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
                // Socket u� je uzav�en�
                return;
            }
            catch (Exception ex)
            {
                // Zpracovat chybu, p��padn� zkusit znovu
                this.Invoke((Action)(() =>
                    listMessages.Items.Add($"Chyba: {ex.Message}")
                ));
                udpBeginReceive();
                return;
            }

            // Dek�dujeme zpr�vu a p�id�me na UI
            string msg = Encoding.UTF8.GetString(buffer);
            this.Invoke((Action)(() =>
                listMessages.Items.Add("Recv: " + msg)
            ));

            if (!msg.StartsWith("potvrzeno:") && !msg.StartsWith("acknowledged:"))
            {
                // Odpov�me potvrzen�m
                string confirmation = "potvrzeno: " + remoteEP.Address.ToString();
                byte[] confirmationData = Encoding.UTF8.GetBytes(confirmation);
                _udpClient.Send(confirmationData, confirmationData.Length, remoteEP);
            }

            // Zaregistrujeme p��jem dal�� zpr�vy
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
                // Adresa a port p��jemce
                IPAddress remoteIp = IPAddress.Parse(textIPadr.Text);
                int remotePort = Convert.ToInt32(textPort.Text);

                var endpoint = new IPEndPoint(remoteIp, remotePort);
                _udpClient.Send(data, data.Length, endpoint);
            }
            catch (FormatException)
            {
                MessageBox.Show("Neplatn� IP adresa.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Odesl�n� selhalo: {ex.Message}");
            }
        }
    }
}
