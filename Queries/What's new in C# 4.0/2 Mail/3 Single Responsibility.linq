<Query Kind="Program">
  <Namespace>System.Net.Mail</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

class MailMessage
{
	public string From, To, Subject, Body, CC, Bcc, ReplyTo;
	public bool IsBodyHtml;
	public DeliveryNotificationOptions DeliveryNotificationOptions;
	public MailPriority Priority;
}

class SmtpClient
{
	public string Host;
	public int Port = 25;
	public ICredentialsByHost Credentials;
	public SmtpDeliveryMethod DeliveryMethod = SmtpDeliveryMethod.Network;
	public bool EnableSSL;
	public int TimeOut;
	
	public void Send (MailMessage mailMessage) { /* ...... */ }
}

void Main()
{
	var mm = new MailMessage { From = "mail@albahari.com", To = "brian.madsen@live.com", Body = "Hi, Brian!" };
	new SmtpClient { Host = "mail.iinet.net.au" }.Send (mm);
}