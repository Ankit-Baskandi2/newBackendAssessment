
namespace AssessementProjectForAddingUser.Domain.HelperClass
{
    public static class HtmlBodyForSendinEmailCredentails
    {
        public static string EmailHtmlWithCredentails(string name, string email, string password)
        {
            return string.Format(@"
                                <!DOCTYPE html>
                                <html>
                                    <head>
                                        <title>Activate Your Account</title>
                                            <style>
                                                body {margin: 0;
                                                padding: 0;
                                                font-family: Arial, Helvetica, sans-serif;
                                                }
                                                .container {height: auto;
                                                background: linear-gradient(to top, #c9c9ff 50%, #6e6ef6 90%) no-repeat;
                                                width: 400px;
                                                padding: 30px;
                                                }
                                            </style>
                                    </head>
                                    <body>
                                      <div class=""container"">
                                        <div>
                                          <h1>Dear {0}</h1>
                                          <h1>Activate your account</h1>
                                          <hr>
                                          <p>You're Login credentials are below :</p>
                                          <p>Email : {1}</p>
                                          <p>Password : {2}</p>
                                          <p>Do not share your details with others</p>
                                        </div>
                                      </div>
                                    </body>
                                </html>", name, email, password);
        }
    }
}
