using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;

namespace WeatherApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        string APIkey = "ca609de96b11cc948883645f5e235ee8";
        private void btn_search_Click(object sender, EventArgs e)
        {
            getWeather();
        }

        private void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", TbCity.Text, APIkey);
                var json = web.DownloadString(url);
                Weatherinfo.root Info = JsonConvert.DeserializeObject<Weatherinfo.root>(json);
                pic_icon.ImageLocation = "https://openweathermap.org/img/w/"+Info.weather[0].icon+".png";
                lab_condition.Text = Info.weather[0].main;
                lab_detail.Text = Info.weather[0].description;
                lab_sunset.Text = convertDateTime(Info.sys.sunset).ToString();
                lab_sunrise.Text = convertDateTime(Info.sys.sunrise).ToString();
                lab_windspeed.Text = Info.wind.speed.ToString();
                lab_pressure.Text = Info.main.pressure.ToString();
            }
            DateTime convertDateTime(long millisec)
            {
                DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                day = day.AddSeconds(millisec).ToLocalTime();
                return day;
            }
        }
    }
}
