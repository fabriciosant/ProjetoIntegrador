using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoKMais.Models
{
    public class CPF
    {
        public const int ValorMaxCpf = 11;
        public const int ValorMaxCpfComMascara = 14;
        public string Codigo { get; private set; }

        protected CPF() { }

        public CPF(string cpf)
        {
            Codigo = cpf;
            if (!ValidandoCaracteres()) Invalidar();
        }

        public bool ValidandoCaracteres()
        {
            var cpf = Codigo;

            if (!string.IsNullOrEmpty(cpf))
            {
                if (cpf.Length == ValorMaxCpf &&
                    cpf.All(char.IsDigit) && Validar(cpf))
                    return true;

                if (cpf.Length == ValorMaxCpfComMascara)
                {
                    SemFormatacao();
                    cpf = Codigo;
                    if (cpf.All(char.IsDigit) && Validar(cpf))
                        return true;
                }
            }

            return false;
        }

        public string ComFormatacao()
        {
            return Convert
                .ToUInt64(Codigo)
                .ToString(@"000\.000\.000\-00");
        }

        public void SemFormatacao()
        {
            if (Codigo.Length == ValorMaxCpf) return;

            Codigo = Codigo
                .Replace(".", string.Empty)
                .Replace("-", string.Empty);
        }
        
        public static string TirarFormatacao(string cpf)
        {
            if (cpf.Length == ValorMaxCpf) return cpf;

            return cpf
                .Replace(".", string.Empty)
                .Replace("-", string.Empty);
        }



        public static bool Validar(string cpf)
        {
            if (cpf.Length > ValorMaxCpf)
                return false;

            while (cpf.Length != ValorMaxCpf)
                cpf = '0' + cpf;

            var igual = true;
            for (var i = 1; i < ValorMaxCpf && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            var numeros = new int[ValorMaxCpf];

            for (var i = 0; i < ValorMaxCpf; i++)
                numeros[i] = int.Parse(cpf[i].ToString());

            var soma = 0;
            for (var i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            var resultado = soma % ValorMaxCpf;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != ValorMaxCpf - resultado)
                return false;

            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += (ValorMaxCpf - i) * numeros[i];

            resultado = soma % ValorMaxCpf;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else if (numeros[10] != ValorMaxCpf - resultado)
                return false;

            return true;
        }

        public void Invalidar()
        {
            throw new Exception("CPF invalido!");
        }
    }
}
