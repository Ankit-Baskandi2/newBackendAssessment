
namespace AssessementProjectForAddingUser.Domain.HelperClass
{
    public static class HtmlForEmail
    {
        public static string EmailStringBody(string email, string emailToken)
        {
            return $@"<html>
           <head></head>      
          <body style="" margin:0;padding:0;font-family:Arial,Helvetica,sans-serif;"">
           <div style=""height:auto;background: linear-gradient(to top, #c9c9ff 50%,#6e6ef6 90%) no-repeat;width:400px;padding:30px;"">
               <div>
                      <div>
                         <h1>Dear ""{email}""</h2>
                         <h1>Activate your account</h1>
                          <hr>
                          <p>You're receiving this mail because your account is not active</p>
                           <p> Please tap the button below to choose a new password.</p>
                           <a href=""http;//localhost:4200/reset?email={email}&code={emailToken}"" target=""_blank"" style=""background:#0d6efc;
                                 color:white;border-radius:4px ;display:block;margin:0 auto;width:50%;text-align:ceneter;text-decoration:none"">Activate Account</a>
                           <p>Kind Regards,<br><br>
                              Ankit Baskandi</p>
                      </div>
               </div>
           </div>
         </body>
         </html>";
        }
    }
}
