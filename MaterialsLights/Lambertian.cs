using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathTracer
{
    public class Lambertian : BxDF
    {
        private Spectrum kd;
        public Lambertian(Spectrum r)
        {
            kd = r;
        }

        // f(wo,wi) in local coords
        public override Spectrum f(Vector3 wo, Vector3 wi)
        {
            /* Implement */
            return kd / Math.PI;
            //return Spectrum.Create(1 / Math.PI);
        }

        // Sample wi direction according to wo in local coords
        public override (Spectrum, Vector3, double) Sample_f(Vector3 wo)
        {
            /* Implement */
            wo = wo.Normalize();
            Vector3 wi = Samplers.CosineSampleHemisphere().Normalize();
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
            //var theta = Math.Asin(Math.Sqrt(Samplers.ThreadSafeRandom.NextDouble()));
            Vector3 N;
            if (wo.z < 0)
            {
                N = new Vector3(0, 0, -1);
            }
            else
            {
                N = new Vector3(0, 0, 1);
            }
            double cosTheta = Vector3.Dot(N, wi)/Math.PI;
            return (cosTheta);
        }
    }
}
