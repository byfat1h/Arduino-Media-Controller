using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace Arduino_Media_Controller
{
    public partial class Form1 : Form
    {
        int butonsayac1 = 0;
        int butonsayac2 = 0;
        int sayac3 = 0;
        String[] portlistesi;
        bool baglanti_durumu = false;
        char data = '0';

        public const int KEYEVENTF_EXTENTEDKEY = 1;
        public const int KEYEVENTF_KEYUP = 0;
        public const int VK_MEDIA_NEXT_TRACK = 0xB0;
        public const int VK_MEDIA_PLAY_PAUSE = 0xB3;
        public const int VK_MEDIA_PREV_TRACK = 0xB1;
        public const int VK_VOLUME_UP = 0xAF;
        public const int VK_VOLUME_DOWN = 0xAE;
        public const int VK_VOLUME_MUTE = 0xAD;
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte virtualKey, byte scanCode, uint flags, IntPtr extraInfo);
        public Form1()
        {
            InitializeComponent();
        }
        void portlistele()
        {
            comboBox1.Items.Clear();
            portlistesi = SerialPort.GetPortNames();
            foreach (string portadi in portlistesi)
            {
                comboBox1.Items.Add(portadi);
                if (portlistesi[0] != null)
                {
                    comboBox1.SelectedItem = portlistesi[0];
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            portlistele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            portlistele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                if (baglanti_durumu == false)
                {
                    serialPort1.PortName = comboBox1.GetItemText(comboBox1.SelectedItem);
                    serialPort1.BaudRate = 9600;
                    if (serialPort1.IsOpen == false)
                    {
                        serialPort1.Open();
                        comboBox1.Enabled = false;
                        button2.Enabled = false;
                        label2.Text = "Connected!";
                        label2.ForeColor = Color.Green;
                        baglanti_durumu = true;
                        button1.ImageIndex = 1;
                        timer1.Start();
                    }
                }
                else
                {
                    if (serialPort1.IsOpen == true)
                    {
                        timer1.Stop();
                        serialPort1.Close();
                        baglanti_durumu = false;
                        label2.Text = "No Connect!";
                        label2.ForeColor = Color.Red;
                        button2.Enabled = true;
                        comboBox1.Enabled = true;
                        button1.ImageIndex = 0;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please selected the port! (Port Seçmeden Bunu Yapamazsın!)");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            next_button();       
        }
     
        private void button7_Click(object sender, EventArgs e)
        {
            playpause_button();          
        }

        private void button5_Click(object sender, EventArgs e)
        {
            prev_button();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mute_button();     
        }

        private void button3_Click(object sender, EventArgs e)
        {
            volume_up_button();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            volume_down_button();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
        }
        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            data = Convert.ToChar(serialPort1.ReadChar());
            this.Invoke(new EventHandler(displayData_event));
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPort1_DataReceived);
        }
        void next_button()
        {
            keybd_event(VK_MEDIA_NEXT_TRACK, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
            label1.Text = "Music The Skipped";
            button7.ImageIndex = 2;
            butonsayac1 = 1;
        }
        void playpause_button()
        {
            keybd_event(VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
            if (butonsayac1 == 1)
            {
                button7.ImageIndex = 1;
                label1.Text = "Pause The Music";
                butonsayac1 = 0;
            }
            else
            {
                button7.ImageIndex = 2;
                label1.Text = "Play The Music";
                butonsayac1++;
            }
        }
        void prev_button()
        {
            keybd_event(VK_MEDIA_PREV_TRACK, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
            label1.Text = "Previous The Music";
            button7.ImageIndex = 2;
            butonsayac1 = 1;
        }
        void mute_button()
        {
            keybd_event(VK_VOLUME_MUTE, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
            if (butonsayac2 == 1)
            {
                button8.ImageIndex = 1;
                butonsayac2 = 0;
                label1.Text = "UNMUTED";
            }
            else
            {
                button8.ImageIndex = 0;
                label1.Text = "MUTED";
                butonsayac2++;
            }
        }
        void volume_up_button()
        {
            keybd_event(VK_VOLUME_UP, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
            label1.Text = "Volume Up";
            if(sayac3==0)
            {
                button8.ImageIndex = 1;
                butonsayac2 =0;
            }
        }
        void volume_down_button()
        {
            keybd_event(VK_VOLUME_DOWN, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
            label1.Text = "Volume Down";
        }
        private void displayData_event(object sender, EventArgs e)
        {
            switch (data)
            {
                case 'A':
                    playpause_button();
                    break;
                case 'B':
                    next_button();
                    break;
                case 'C':
                    prev_button();
                    break;
                case 'D':
                    volume_up_button();
                    break;
                case 'E':
                    volume_down_button();
                    break;
                case 'F':
                    mute_button();
                    break;
            }
        }
    }
}