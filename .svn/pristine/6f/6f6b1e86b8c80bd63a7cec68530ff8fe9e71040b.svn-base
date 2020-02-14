using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib.Class
{
    public class ProcessTime
    {
        int Tot = 0;
        int Itm = 0;
        double TotRes = 0;
        DateTime st = DateTime.Now;

        public ProcessTime(int tot)
        {
            this.Tot = tot;
        }

        public void Next()
        {
            this.Itm++;

            double sec = DateTime.Now.Subtract(st).TotalSeconds;

            //(15s * (1-(5/100)) * 100) / 5
            double percProc = ((double)Itm / (double)Tot);
            double percFalta = (1 - percProc);
            TotRes = ((double)sec * percFalta) / (double)percProc;
        }

        public string CurrentTime()
        {
            int horas = (int)(TotRes / (60 * 60));
            int minutos = (int)(TotRes / (60));
            int segundos = (int)(TotRes);

            if (TotRes > (60 * 60))
            {
                return (string.Format("registros {0} de {1} falta {2}:{3} horas para acabar", Itm, Tot, horas.ToString("0"), (minutos - (horas * 60)).ToString("00")));
            }
            else if (TotRes > 60)
            {
                return (string.Format("registros {0} de {1} falta {2}:{3} minutos para acabar", Itm, Tot, minutos.ToString("0"), (segundos - (horas * 60)).ToString("00")));
            }
            else
            {
                return (string.Format("registros {0} de {1} falta {2} segundos para acabar", Itm, Tot, TotRes.ToString("0")));
            }
        }
    }
}
