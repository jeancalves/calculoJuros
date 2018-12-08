using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace CalculoJuros
{
        public class Calculo : Notifiable, IValidatable
        {
            public double valorinicial { get; set; }

            public int meses { get; set; }

            public double juros { get { return 0.01; } }

            public double resultado { get; set; }

            public void Validate()
            {
            AddNotifications(
                new Contract()
                    .IsGreaterThan(meses, 0, "meses", "Mes deve ser maior que zero")
                    .IsGreaterThan(valorinicial, 0, "valorinicial", "Valor Inicial deve ser maior que zero")
                );
            }
        }
}
