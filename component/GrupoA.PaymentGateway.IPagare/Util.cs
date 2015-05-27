using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace GrupoA.PaymentGateway.IPagare
{
    public static class Util
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public static string RealizarHttpPost(string uri, IDictionary<string, string> parametros)
        {
            try
            {
                StringBuilder queryString = new StringBuilder();
                foreach (var parametro in parametros)
                {
                    queryString.AppendFormat("{0}={1}&", parametro.Key, parametro.Value);
                }

                // parameters: name1=value1&name2=value2	
                WebRequest webRequest = WebRequest.Create(uri);

                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Method = "POST";

                //byte[] bytes = Encoding.ASCII.GetBytes(queryString.ToString());
                byte[] bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(queryString.ToString());
                Stream os = null;

                try
                {
                    webRequest.ContentLength = bytes.Length;
                    os = webRequest.GetRequestStream();
                    os.Write(bytes, 0, bytes.Length);
                }
                catch (WebException ex)
                {
                    return null;
                }
                finally
                {
                    if (os != null)
                    {
                        os.Close();
                    }
                }

                try
                { // Obtem a resposta.
                    WebResponse webResponse = webRequest.GetResponse();
                    if (webResponse == null)
                    { return null; }
                    StreamReader sr = new StreamReader(webResponse.GetResponseStream(), Encoding.GetEncoding("ISO-8859-1"));
                    return sr.ReadToEnd().Trim();
                }
                catch (WebException ex)
                {
                    return null;
                }
                return null;
            }
            catch (WebException ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que retorna a string correspondente com o tipo de pagamento selecionado.
        /// </summary>
        /// <param name="tipoPagamento"></param>
        /// <returns></returns>
        public static String RetornarTipoPagamento(TipoPagamento tipoPagamento)
        {
            String retorno = String.Empty;

            switch (tipoPagamento)
            {
                case TipoPagamento.AVista:
                    //à vista
                    retorno = "A01";
                    break;
                case TipoPagamento.DuasXSemJuros:
                    //2x sem juros
                    retorno = "A02";
                    break;
                case TipoPagamento.TresXSemJuros:
                    //3x sem juros
                    retorno = "A03";
                    break;
                case TipoPagamento.QuatroXSemJuros:
                    //4x sem juros
                    retorno = "A04";
                    break;
                case TipoPagamento.CincoXSemJuros:
                    //5x sem juros
                    retorno = "A05";
                    break;
                case TipoPagamento.SeisXSemJuros:
                    //6x sem juros
                    retorno = "A06";
                    break;
                case TipoPagamento.SeteXSemJuros:
                    //7x sem juros
                    retorno = "A07";
                    break;
                case TipoPagamento.OitoXSemJuros:
                    //8x sem juros
                    retorno = "A08";
                    break;
                case TipoPagamento.NoveXSemJuros:
                    //9x sem juros
                    retorno = "A09";
                    break;
                case TipoPagamento.DezXSemJuros:
                    //10x sem juros
                    retorno = "A10";
                    break;
                case TipoPagamento.OnzeXSemJuros:
                    //11x sem juros
                    retorno = "A11";
                    break;
                case TipoPagamento.DozeXSemJuros:
                    //12x sem juros
                    retorno = "A12";
                    break;
                case TipoPagamento.DuasXComJuros:
                    //2x Com juros
                    retorno = "B02";
                    break;
                case TipoPagamento.TresXComJuros:
                    //3x Com juros
                    retorno = "B03";
                    break;
                case TipoPagamento.QuatroXComJuros:
                    //4x Com juros
                    retorno = "B04";
                    break;
                case TipoPagamento.CincoXComJuros:
                    //5x Com juros
                    retorno = "B05";
                    break;
                case TipoPagamento.SeisXComJuros:
                    //6x Com juros
                    retorno = "B06";
                    break;
                case TipoPagamento.SeteXComJuros:
                    //7x Com juros
                    retorno = "B07";
                    break;
                case TipoPagamento.OitoXComJuros:
                    //8x Com juros
                    retorno = "B08";
                    break;
                case TipoPagamento.NoveXComJuros:
                    //9x Com juros
                    retorno = "B09";
                    break;
                case TipoPagamento.DezXComJuros:
                    //10x Com juros
                    retorno = "B10";
                    break;
                case TipoPagamento.OnzeXComJuros:
                    //11x Com juros
                    retorno = "B11";
                    break;
                case TipoPagamento.DozeXComJuros:
                    //12x Com juros
                    retorno = "B12";
                    break;
            }

            return retorno;
        }
    }
}
