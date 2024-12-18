//using MedicalWCF.ServiceMethod;
using System;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace MedicalWindowsService
{
    public partial class Service1 : ServiceBase
    {
        Timer timerCorporate = new Timer();
        Timer timerRetail = new Timer();
        //Financial financialService = new Financial();

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("General", "------------------------------------------Service is started at " + DateTime.Now);

            // Timer for Corporate task
            timerCorporate.Elapsed += new ElapsedEventHandler(OnCorporateTask);
            timerCorporate.Interval = Convert.ToDouble(ConfigurationManager.AppSettings["CorporateTimerInterval"]);
            timerCorporate.Enabled = true;

            // Timer for Retail task
            timerRetail.Elapsed += new ElapsedEventHandler(OnRetailTask);
            timerRetail.Interval = Convert.ToDouble(ConfigurationManager.AppSettings["RetailTimerInterval"]);
            timerRetail.Enabled = true;
        }

        protected override void OnStop()
        {
            WriteToFile("General", "------------------------------------------Service is stopped at " + DateTime.Now);
            timerCorporate.Enabled = false;
            timerRetail.Enabled = false;
        }


        private void OnCorporateTask(object source, ElapsedEventArgs e)
        {
            WriteToFile("Corporate", "-------Start: LoadCustomerCorporateToKafka execution at " + DateTime.Now);
            try
            {
                //financialService.LoadCustomerCorporateToKafka();
            }
            catch (Exception ex)
            {
                WriteToFile("Corporate", "Error in LoadCustomerCorporateToKafka: " + ex.Message);
            }
            WriteToFile("Corporate", "-------End: LoadCustomerCorporateToKafka execution at " + DateTime.Now);
        }


        private void OnRetailTask(object source, ElapsedEventArgs e)
        {
            WriteToFile("Retail", "-------Start: LoadCustomerRetailToKafka execution at " + DateTime.Now);
            try
            {
                //financialService.LoadCustomerRetailToKafka();
            }
            catch (Exception ex)
            {
                WriteToFile("Retail", "Error in LoadCustomerRetailToKafka: " + ex.Message);
            }
            WriteToFile("Retail", "-------End: LoadCustomerRetailToKafka execution at " + DateTime.Now);
        }


        public void WriteToFile(string fileIdentifier, string message)
        {
            string path = ConfigurationManager.AppSettings["WSLogs"];
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // File name based on the identifier (Corporate, Retail, General)
            string filepath = path + $"\\{fileIdentifier}_Log_{DateTime.Now.ToShortDateString().Replace('/', '_')}.txt";

            // Append to the file
            using (StreamWriter sw = File.AppendText(filepath))
            {
                sw.WriteLine(message);
            }
        }
    }

}