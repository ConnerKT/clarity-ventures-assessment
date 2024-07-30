# clarity-ventures-assessment

Add packages to your Library to send the Emails

dotnet add package Microsoft.Extensions.Configuration

dotnet add package Sendgrid

dotnet add package Microsoft.AspNetCore.Identity

## For Api

In Sendgrid, create your trial account and create an API Key. Then add your SendGrid API key to your appsettings.json file

"SendGrid": { "SecretKey": "" }

[Docs](https://www.twilio.com/docs/sendgrid/for-developers/sending-email/email-api-quickstart-for-c)