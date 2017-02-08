<Query Kind="Program">
  <Namespace>System.Net.Mail</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void SendMail (
	string smtpHost,
	string from,
	string to,
	string subject = "",
	string body = "",
	string cc = "",
	string bcc = "",
	string replyTo = "",
	bool isBodyHtml = false,	
	DeliveryNotificationOptions deliveryNotificationOptions = DeliveryNotificationOptions.None,
	MailPriority priority = MailPriority.Normal,
	int smtpPort = 25,
	ICredentialsByHost credentials = null,
	SmtpDeliveryMethod deliveryMethod = SmtpDeliveryMethod.Network,
	bool enableSSL = false,
	int timeOut = 0)
{
	// ...
}

void Main() { }