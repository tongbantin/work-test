using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Net.Mail;
using log4net;

namespace KKB.Treasury.TreasuryCommon.Common
{
    public class Encryption
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Encryption));

        //Encrypt folder with password
        public static void EncryptByFolder(string pathfile, string folder, string password)
        {
            try
            {
                string fullpath = pathfile + folder + "/";

                if (File.Exists(pathfile + folder + ".zip"))
                    File.Delete(pathfile + folder + ".zip");

                    using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                    {
                        zip.UseUnicodeAsNecessary = true;  //utf-8
                        zip.Password = password;
                        zip.AddDirectory(pathfile + folder);

                        zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                        zip.Save(pathfile + folder + ".zip");
                    }

                    //Clear File After Zip
                    if (Directory.Exists(pathfile + folder))
                    {
                        string[] f = Directory.GetFiles(pathfile + folder);
                        for (int l = 0; l < f.Length; l++)
                        {
                            File.Delete(f[l]);
                        }
                        Directory.Delete(pathfile + folder);
                    }
               // }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        
        //Encrypt files in folder with different password

      
        //Encrypt files in folder with same password
          public static void EncryptByFile(string pathfile, string filename, string uniqueItems)
        {
            try
            {

                if (File.Exists(pathfile + "/" + filename + ".zip"))
                    File.Delete(pathfile + "/" + filename + ".zip");

                    using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                    {
                        zip.UseUnicodeAsNecessary = true;  //utf-8
                        zip.Password = uniqueItems;
                        zip.AddDirectory(pathfile);

                        //zip by file
                        //zip.AddFile(Path + "/" + DateTime.Now.ToString("yyyyMMdd")+ "/" + prefixEncrypt[i] + "_" + filename + ".pdf","" );

                        zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                        zip.Save(pathfile + "/" + filename + ".zip");
                    }

                    //Clear File After Zip
                    if (Directory.Exists(pathfile + "/" + filename))
                    {
                        string[] f = Directory.GetFiles(pathfile + "/" + filename);
                        for (int l = 0; l < f.Length; l++)
                        {
                            File.Delete(f[l]);
                        }
                        Directory.Delete(pathfile + "/" + filename);
                    }
               // }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        //Send mail by webparam address
          public static void SendMail(string host, int port, string From, string To, string Cc, string Subject,string Message , List<Attachment> Attach)
          {
              try
              {
                  //Parameter
                  string to = To;
                  bool isHtml = true;
                  string message = Message;

                  using (MailMessage mail = new MailMessage())
                  {
                      mail.From = new MailAddress(From);
                      string[] tto = to.Split(',');
                      for (int i = 0; i < tto.Count(); i++)
                      {
                          mail.To.Add(new MailAddress(tto[i].Replace("\n", "")));
                      }
                      if (!Cc.Trim().Equals(""))
                      {
                          string[] ccto = Cc.Split(',');
                          for (int j = 0; j < ccto.Count(); j++)
                          {
                              mail.CC.Add(new MailAddress(ccto[j].Replace("\n", "")));
                          }
                      }
                      // Define the message
                      mail.Subject = Subject;
                      mail.IsBodyHtml = isHtml;
                      mail.Body = message;
                      foreach (Attachment a in Attach)
                      {
                          mail.Attachments.Add(a);
                      }

                      var mailclient = new SmtpClient();
                      mailclient.Host = host;
                      mailclient.Port = port;
                      mailclient.EnableSsl = false;
                      mailclient.UseDefaultCredentials = true;

                      mailclient.Send(mail);
                  }
              }
              catch (Exception ex)
              {
                  throw ex;
                  Log.Error(ex.Message);
              }

          }
    }
}

