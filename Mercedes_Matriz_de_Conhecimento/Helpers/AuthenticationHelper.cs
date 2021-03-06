﻿//using Mercedes_Matriz_de_Conhecimento.Models;
//using System;
//using System.Net;
//using System.Web;
//using System.Web.Configuration;
//using System.Web.Security;

//namespace Mercedes_Matriz_de_Conhecimento.Helpers
//{
//    public static class AuthenticationHelper
//    {
//        private static HttpContext _context { get { return HttpContext.Current; } }
//        private static string MatrizCookieName = "MatrizDeConhecimento";


//        public static Account GetCurrentUser()
//        {
//            try
//            {
//                var cookie = CookieHelper.Get(MatrizCookieName);

//                if (cookie != null && !string.IsNullOrEmpty(cookie))
//                {

//                    var retorno = mapTicket(FormsAuthentication.Decrypt(cookie));

//                    return retorno;
//                }

//                return null;
//            }

//            catch (Exception)
//            {

//                return null;

//            }
//        }

//        public static void RegistrarAutenticacao(string nome, string chave, string usuarioId, string nivelAcesso, int timeout, bool persisteCookie)
//        {
//            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, chave, DateTime.Now, DateTime.Now.AddMinutes(timeout),
//                                                                             persisteCookie, $"{usuarioId}|{nivelAcesso}|{chave}", FormsAuthentication.FormsCookiePath);


//            string encTicket = FormsAuthentication.Encrypt(ticket);
//            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
//            //cookie.Domain = ".dc.com.br";
//            cookie.HttpOnly = true;
//            _context.Response.Cookies.Add(cookie);
//        }

//        public static void LimparRegistroAutenticacao()
//        {
//            // clear authentication cookie
//            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
//            cookie1.Expires = DateTime.Now.AddYears(-1);
//            _context.Response.Cookies.Add(cookie1);

//            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
//            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
//            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
//            cookie2.Expires = DateTime.Now.AddYears(-1);
//            _context.Response.Cookies.Add(cookie2);


//            // clear GSA cookie
//            HttpCookie cookie3 = new HttpCookie(MatrizCookieName, "");
//            cookie3.Expires = DateTime.Now.AddYears(-1);
//            _context.Response.Cookies.Add(cookie3);

//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="usuarioId">ID do Usuário</param>
//        /// <param name="nivelAcesso">Nível de Acesso</param>
//        /// <param name="chave">Chave AMÉRICAS do usuário</param>
//        /// <param name="nome">Nome do Usuário</param>
//        /// <param name="diretoriaId">Id da Diretoria do Usuário</param>
//        /// <param name="centroCustoId">Id do Centro de Custo do Usuário</param>
//        public static void RegisterGSAUserData(string chave, string nome)
//        {
//            CookieHelper.Set(MatrizCookieName, $"|{chave}|{ImageUrlExists(chave)}|{nome}");
//        }


//        /// <summary>
//        /// Retorna a chave americas do usuário logado no sistema.
//        /// </summary>
//        /// <returns>Username do usuário</returns>
//        public static string ObterUsuarioChave()
//        {
//            string chave = string.Empty;

//            if (_context.User.Identity.IsAuthenticated)
//            {
//                var cookieValue = CookieHelper.Get(MatrizCookieName);
//                string[] dadosUsuario = cookieValue.Split('|');
//                if (dadosUsuario.Length > 2)
//                {
//                    chave = dadosUsuario[2].ToLower();
//                }
//            }

//            return chave;
//        }


//        /// <summary>
//        /// Retorna a url/path da imagem do usuário logado
//        /// </summary>
//        /// <returns>Url/Path com caminho da imagem do usuario</returns>
//        /// 

//        //private static Account mapTicket(FormsAuthenticationTicket ticket)
//        //{
//        //    var data = ticket.UserData.Split('|');
//        //    var model = new Account()
//        //    {
//        //        Name = data[0],
//        //        Username = ticket.Name,
//        //        AccessLevel = (Account.AccessLevels)Enum.Parse(typeof(Account.AccessLevels), data[2]),
//        //        PhotoUrl = data[3]
//        //    };
//        //    model.SetControllersByAction(data[4]);
//        //    return model;
//        //}

//        //public static string GetUrlPathImage()
//        //{
//        //    string imagePath = string.Empty;
//        //    if (_context.User.Identity.IsAuthenticated)
//        //    {
//        //        var cookieValue = CookieHelper.Get(MatrizCookieName);
//        //        string[] userData = cookieValue.Split('|');
//        //        if (userData.Length > 3)
//        //        {
//        //            imagePath = userData[3];
//        //        }
//        //    }
//        //    return imagePath;
//        //}


//        //private static bool GetPictureSize(string url)
//        //{
//        //    HttpWebRequest wreq;
//        //    HttpWebResponse wresp;
//        //    System.IO.Stream mystream;
//        //    System.Drawing.Bitmap bmp;

//        //    bmp = null;
//        //    mystream = null;
//        //    wresp = null;
//        //    try
//        //    {
//        //        wreq = (HttpWebRequest)WebRequest.Create(url);
//        //        wreq.AllowWriteStreamBuffering = true;

//        //        wresp = (HttpWebResponse)wreq.GetResponse();

//        //        if ((mystream = wresp.GetResponseStream()) != null)
//        //            bmp = new System.Drawing.Bitmap(mystream);
//        //    }
//        //    catch (Exception er)
//        //    {
//        //        return false;
//        //    }
//        //    finally
//        //    {
//        //        if (mystream != null)
//        //            mystream.Cancelar();

//        //        if (wresp != null)
//        //            wresp.Cancelar();
//        //    }

//        //    if (bmp.Width <= 1)
//        //    {
//        //        return false;
//        //    }
//        //    return true;
//        //}

//        public static string GetUserImage(string userName)
//        {
//            string imagePath = "";

//            if (userName.Length > 1)
//            {
//                userName = userName.ToLower();
//                imagePath = string.Format("http://intra-wiw.e.corpintra.net/wiw/photo/{0}.jpg", userName);
//            }
//            else
//            {
//                imagePath = "~/Imagens/user-default.png";
//            }
//            return imagePath;
//        }

//    }
//}