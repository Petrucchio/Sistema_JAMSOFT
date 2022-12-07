using Sistema_simples.Context;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Sistema_simples.Controllers
{
    public class ValidacaoCustomizada
    {
        public class ValidarCPf : ValidationAttribute
        {
            public static bool CpfValidation(string cpf)
            {
                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;
                cpf = cpf.Trim();
                cpf = cpf.Replace(".", "").Replace("-", "");
                if (cpf.Length != 11)
                    return false;
                if (cpf == "00000000000" ||
                    cpf == "11111111111" ||
                    cpf == "22222222222" ||
                    cpf == "33333333333" ||
                    cpf == "44444444444" ||
                    cpf == "55555555555" ||
                    cpf == "66666666666" ||
                    cpf == "77777777777" ||
                    cpf == "88888888888" ||
                    cpf == "99999999999")
                    return false;
                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return cpf.EndsWith(digito);

            }
            public static bool CnpjValidation(string cnpj)
            {
                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int soma;
                int resto;
                string digito;
                string tempCnpj;
                cnpj = cnpj.Trim();
                cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
                if (cnpj.Length != 14)
                    return false;
                tempCnpj = cnpj.Substring(0, 12);
                soma = 0;
                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCnpj = tempCnpj + digito;
                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return cnpj.EndsWith(digito);
            }

            public override bool IsValid(object value)
            {
                if (value == null || string.IsNullOrEmpty(value.ToString()))
                    return false;
                if (value.ToString().Length >= 14 && value.ToString().Contains("/"))
                    return CnpjValidation(value.ToString());
                return CpfValidation(value.ToString());
            }
            
        }
        public class ValidarDevolucao : ValidationAttribute
        {
            public static bool devolucaoValidation(DateTime data)
            {
                if (data < DateTime.Now)
                {
                    return false;
                }
                return true;
            }
            public override bool IsValid(object value)
            {
                if (value == null )
                    return false;

                return devolucaoValidation(DateTime.Parse(value.ToString()));
            }
        }

        public class ValidarQuantidade : ValidationAttribute
        {
            public static bool QuantidadeValidation(int quantidade)
            {
                if (quantidade > 0)
                {
                    return true;
                }
                return false;
                
            }
            public override bool IsValid(object value)
            {
                if (value == null)
                    return false;

                return QuantidadeValidation(Convert.ToInt16(value));
            }
        }

    }
}
