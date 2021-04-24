using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;

public class SendMailByMailMessage : MonoBehaviour
{
    string _sender = "portusvrtest@gmail.com";
    string _password = "fghkj)f0G";

    public void SendEmail(string recipient, string pathToFile)
    {
        Debug.Log("SEND EMAIL FUNCTION EXECUTED");
        //For File Attachment, more files can also be attached
        Attachment att = new Attachment(pathToFile);
        //tested only for files on local machine
        //Hardcoded recipient email and subject and body of the mail
        string subject = "Testmail from App";
        string message = "Test_Hello_World";
        SmtpClient client = new SmtpClient("smtp.gmail.com");
        //SMTP server can be changed for gmail, yahoomail, etc., just google it up
        client.Port = 587;
        //client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.UseDefaultCredentials = false;
        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(_sender, _password);
        client.EnableSsl = true;
        client.Credentials = (System.Net.ICredentialsByHost)credentials;
        ServicePointManager.ServerCertificateValidationCallback = (x, y, z, w) => true;
        try
        {
            var mail = new MailMessage(_sender.Trim(), recipient.Trim());
            mail.Subject = subject;
            mail.Body = message;
            mail.Attachments.Add(att);
            Debug.Log("Attachment is now Online");
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            client.Send(mail);
            Debug.Log("Success");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            throw ex;
        }
    }
}
