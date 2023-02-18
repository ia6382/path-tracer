using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathTracer
{
    public class PhongBlinn : BxDF
    {
        double alfa = 200;
        private Spectrum kd;
        private Spectrum ks;
        public PhongBlinn(Spectrum r)
        {
            kd = r;
            ks = Spectrum.Create(0.9);
        }

        // f(wo,wi) in local coords
        public override Spectrum f(Vector3 wo, Vector3 wi)
        {
            /* Implement */
            //kd = kd / Math.PI;
            //ks = ks*(Spectrum.Create(Pdf(wo, wi)));

            //return kd.AddTo(ks);
            return (kd / Math.PI).AddTo(ks * (Spectrum.Create(Pdf(wo, wi))));
        }

        // Sample wi direction according to wo in local coords
        public override (Spectrum, Vector3, double) Sample_f(Vector3 wo)
        {
            /* Implement */
            wo = wo.Normalize();
            Vector3 wi = Samplers.PhongBlinnSample(alfa).Normalize();
            //Vector3 wi = Samplers.CosineSampleHemisphere().Normalize();
            if (wo.z < 0)
            {
                wi = wi * (-1);
            }
            Spectrum k = f(wo, wi);
            double p = Pdf(wo, wi);

            return (k, wi, p);
        }

        // pdf(wo,wi) in local coords
        public override double Pdf(Vector3 wo, Vector3 wi)
        {
            /* Implement */
            Vector3 V = wo.Normalize();
            Vector3 L = wi.Normalize();
            Vector3 N;
            if (wo.z < 0)
            {
                N = new Vector3(0, 0, -1);
            }
            else
            {
                N = new Vector3(0, 0, 1);
            }


            Vector3 H = (L + V).Normalize();
            double cosThetaH = Vector3.Dot(N, H);

            return alfa * ((alfa + 1) / 2 * Math.PI) * Math.Pow(cosThetaH, alfa);
        }
    }
}
