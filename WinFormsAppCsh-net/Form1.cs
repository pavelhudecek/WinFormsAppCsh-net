using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Drawing.Text;

namespace WinFormsAppCsh_net
{
    public partial class Form1 : Form
    {
        private UdpClient udpClient;
        private TcpListener tcpListener;
        private TcpClient tcpClient;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void udpBeginReceive()
        {
            // zaregistrujeme callback, kter� se spust� p�i p�ijet� paketu
            udpClient.BeginReceive(ReceiveCallback, null);
        }

        private void buttonUDPstart_Click(object sender, EventArgs e)
        {
            // Pokud chcete z�rove� IPv4 i IPv6 p��jem, pou�ijte DualMode
            udpClient = new UdpClient(AddressFamily.InterNetworkV6);
            udpClient.Client.DualMode = true;   // umo�n� p�ij�mat i IPv4
            udpClient.Client.Bind(new IPEndPoint(IPAddress.IPv6Any, Convert.ToInt32(textPort.Text)));
            udpBeginReceive();

            listStat.Items.Add("udpClient spu�t�n.");
            buttonUDPstart.Enabled = false;
            buttonStop.Enabled = true;
            buttonSendMsg.Enabled = true;
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            IPEndPoint remoteEP = null;
            byte[] buffer;

            try
            {
                buffer = udpClient.EndReceive(ar, ref remoteEP);
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
                listMessages.Items.Add("Recv> " + msg)
            ));

            if (!msg.StartsWith("#ack:"))
            {
                // Odpov�me potvrzen�m
                string confirmation = "#ack:" + remoteEP.ToString();
                byte[] confirmationData = Encoding.UTF8.GetBytes(confirmation);
                udpClient.Send(confirmationData, confirmationData.Length, remoteEP);
            }

            // Zaregistrujeme p��jem dal�� zpr�vy
            udpBeginReceive();
        }

        private void buttonTCPstart_Click(object sender, EventArgs e)
        {

        }

        private void buttonTCPconn_Click(object sender, EventArgs e)
        {

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (udpClient != null)
            {
                udpClient.Close();
                udpClient.Dispose();
                listStat.Items.Add("udpClient ukon�en.");
            }

            buttonUDPstart.Enabled = true;
            buttonTCPstart.Enabled = true;
            buttonSendMsg.Enabled = false;
        }

        private void buttonSendMsg_Click(object sender, EventArgs e)
        {
            IPAddress remoteIp = null;

            if (udpClient != null)
            {
                try
                {

                    // Adresa a port p��jemce

                    remoteIp = IPAddress.Parse(textIPadr.Text);
                }
                catch (FormatException)
                {
                    listStat.Items.Add("Neplatn� IP adresa.");
                }
                try
                {
                    if (remoteIp == null || remoteIp.AddressFamily != AddressFamily.InterNetworkV6 && remoteIp.AddressFamily != AddressFamily.InterNetwork)
                    {
                        listStat.Items.Add("remoteIP neni");
                        IPAddress[] addresses = Dns.GetHostAddresses(textIPadr.Text);
                        remoteIp = Array.Find(addresses, a => a.AddressFamily == AddressFamily.InterNetworkV6);
                        if (remoteIp == null || remoteIp.AddressFamily != AddressFamily.InterNetworkV6)
                        {
                            listStat.Items.Add("remoteIP neni v6");
                            remoteIp = Array.Find(addresses, a => a.AddressFamily == AddressFamily.InterNetwork);
                            listStat.Items.Add("addresses len: " + addresses.Length + "remoteIP" + remoteIp.ToString());
                        }
                    }

                    int remotePort = Convert.ToInt32(textPort.Text);
                    var endpoint = new IPEndPoint(remoteIp, remotePort);

                    byte[] data = Encoding.UTF8.GetBytes(textSendMsg.Text);
                    udpClient.Send(data, data.Length, endpoint);

                    listMessages.Items.Add("Sent:" + remoteIp.ToString() + ">" + textSendMsg.Text);
                }
                catch (FormatException)
                {
                    listStat.Items.Add("Neplatn� IP adresa.");
                }
                catch (Exception ex)
                {
                    listStat.Items.Add($"Odesl�n� selhalo: {ex.Message}");
                }

            }
            if (tcpClient != null && tcpClient.Connected)
            {
                try
                {
                    NetworkStream stream = tcpClient.GetStream();
                    byte[] data = Encoding.UTF8.GetBytes(textSendMsg.Text);
                    stream.Write(data, 0, data.Length);
                    listMessages.Items.Add("Sent: " + textSendMsg.Text);
                }
                catch (Exception ex)
                {
                    listStat.Items.Add($"Odesl�n� TCP zpr�vy selhalo: {ex.Message}");
                }
            }

        }

        private void buttonStat_Click(object sender, EventArgs e)
        {
            if (udpClient!=null)
            {
                listStat.Items.Add("udpClient:");
                listStat.Items.Add($"AddressFamily:{udpClient.Client.AddressFamily.ToString()}");
                if (udpClient.Client.LocalEndPoint != null) listStat.Items.Add($"LocalEndPoint:{udpClient.Client.LocalEndPoint.ToString()}");
                listStat.Items.Add($"LocalAddressFamily:{udpClient.Client.AddressFamily.ToString()}");
                listStat.Items.Add($"DualMode:{udpClient.Client.DualMode.ToString()}");
                listStat.Items.Add($"EnableBroadcast:{udpClient.Client.EnableBroadcast.ToString()}");
                listStat.Items.Add($"ExclusiveAddressUse:{udpClient.Client.ExclusiveAddressUse.ToString()}");
                listStat.Items.Add($"ProtocolType:{udpClient.Client.ProtocolType.ToString()}");
                listStat.Items.Add($"Ttl:{udpClient.Client.Ttl.ToString()}");
                if (udpClient.Client.RemoteEndPoint!=null) listStat.Items.Add($"RemoteEndPoint:{udpClient.Client.RemoteEndPoint.ToString()}");
                listStat.Items.Add($"SocketType:{udpClient.Client.SocketType.ToString()}");    
            } else
            {
                listStat.Items.Add("(udpClient nen� inicializov�n.)");
            }

        }
    }
}
