using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessLogicalLayer.Helper
{
    public static class BuscaHelper
    {
        private static String[] StopWords = { "about", "1", "after", "2", "all", "also", "3", "an", "4", "and", "5", "another", "6", "any", "7", "are", "8", 
                                   "as", "9", "at", "0", "be", "$", "because", "been", "before", "being", "between", "both", "but", "by", "came", 
                                   "can", "come", "could", "did", "do", "does", "each", "else", "for", "from", "get", "got", "has", "had", "he", 
                                   "have", "her", "here", "him", "himself", "his", "how", "if", "in", "into", "is", "it", "its", "just", "like", 
                                   "make", "many", "me", "might", "more", "most", "much", "must", "my", "never", "no", "now", "of", "on", "only", 
                                   "or", "other", "our", "out", "over", "re", "said", "same", "see", "should", "since", "so", "some", "still", 
                                   "such", "take", "than", "that", "the", "their", "them", "then", "there", "these", "they", "this", "those", "through", 
                                   "to", "too", "under", "up", "use", "very", "want", "was", "way", "we", "well", "were", "what", "when", "where", 
                                   "which", "while", "who", "will", "with", "would", "you", "your", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", 
                                   "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "%", "<", ">", "'", "*", "/", @"\",
                                   "&", "(", ")", "de", "com", "em", "na", "no", "  ", @"""" };

        /// <summary>
        /// Método que retorna array de palavras chave da busca de títulos
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static String[] RemoveStopWords(string texto)
        {
            String[] textoComparacao = texto.ToLower().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<String> textoParaBusca = (from p in textoComparacao where p.Length > 1 select p).Except(StopWords);
            return textoParaBusca.ToArray<String>();
        }

        public static String FormataPalavraFiltro(String palavra)
        {
            if (!String.IsNullOrEmpty(palavra))
            {
                String[] arrPalavra = RemoveStopWords(palavra);

                if (arrPalavra.Count() > 0)
                {
                    var res = arrPalavra.Aggregate((current, next) => current + " AND " + next);
                    palavra = res.ToString();
                    palavra = palavra.Replace("'", "");
                    palavra = palavra.Replace(" AND ", "*\" AND \"");
                    palavra = "\"" + palavra + "*\"";
                    return palavra;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return palavra;
            }
        }
    }
}
