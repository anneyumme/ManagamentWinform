using System;
using System.Management;
using System.Net;
using DevExpress.ChartRangeControlClient.Core;
using UserManagament.Model;

namespace UserManagament
{
    // This class used as a way to get information of user when they login 
    public class LoginInformation
    {
        UserManagament.Model.MidTermEntities dbContext = new Model.MidTermEntities();

        public void Record(User_List user)
        {
            UserManagament.Model.MidTermEntities dbContext = new MidTermEntities();

            // Get timestamp
            DateTime currentTimestamp = DateTime.Now;
            string timestampString = currentTimestamp.ToString("yyyy-MM-dd HH:mm:ss");

            // Get IP
            string ipAddress = GetPublicIPAddress();
            // Get Os info
            string osInfo = GetOperatingSystemInfo();

            // Save data

            String LogInfo = user.First_name + " | " + currentTimestamp + " | " + ipAddress + " | " + osInfo;

            var NewLoginHistory = new Login_History() { };
            NewLoginHistory.ID_user = user.ID;
            NewLoginHistory.Info_login = LogInfo;

            dbContext.Login_History.Add(NewLoginHistory);
            dbContext.SaveChanges();
        }

        private string GetPublicIPAddress()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string publicIPAddress = client.DownloadString("https://api.ipify.org");
                    return publicIPAddress;
                }
            }
            catch (Exception ex)
            {
                 return "Unknown";
            }
        }
        private  string GetOperatingSystemInfo()
        {
            try
            {
                ManagementObjectSearcher searcher = new 
                    ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectCollection osCollection = searcher.Get();

                foreach (ManagementObject mo in osCollection)
                {
                    string caption = mo["Caption"].ToString();
                    return caption;
                }

                return "Unknown";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}