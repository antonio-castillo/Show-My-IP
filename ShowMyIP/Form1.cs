using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.Sockets;

namespace ShowMyIP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Recoge el host del equipo y lo settea al label1
            String Hostname = Dns.GetHostName();
            label1.Text = "Hostname: " + Hostname;
           

            //Nos indica cual es la Ip publica, haciento referencia al metodo creado.
            String WAN = GetIPAddress();
            label3.Text = "WAN IP: " + WAN;


            label2.Text = "Private IP: " + LocalIPAddress().ToString();

        }

        //Recoge la ip privada por defecto del equipo.
        private IPAddress LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }




        //Metodo para sacar ip publica
        static string GetIPAddress()
        {
            String address = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return address;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
