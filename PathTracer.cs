using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PathTracer.Samplers;

namespace PathTracer
{
  class PathTracer
  {
    public Spectrum Li(Ray r, Scene s)
    {
            //result L and accumulator B
            var L = Spectrum.ZeroSpectrum;
            var B = Spectrum.Create(1);
            bool iss = false;
            for (int bounces = 0; bounces < 20; ++bounces)
            {
                //izracunaj presecisce
                SurfaceInteraction intersect;
                double? d;
                (d, intersect) = s.Intersect(r);

                //ce ni presecisca
                if (intersect == null) {
                    break;
                }

                Vector3 wo = -(r.d);
                
                //ce smo zadeli luc
                if (intersect.Obj is Light) {
                    if (bounces == 0 || iss) //samo ce smo prvic zadeli luc
                    {
                        //L = B * intersect.Le(wo);
                        L.AddTo(B * intersect.Le(wo));
                    }
                    break;
                }
                //ce smo zadeli objekt
                Shape intersectShape = (Shape)intersect.Obj;
                iss = intersectShape.BSDF.IsSpecular;

                //path reuse - sample from random light to current intersect
                if (!intersectShape.BSDF.IsSpecular) // preverimo da ni specular material
                {
                    Spectrum Ld = Light.UniformSampleOneLight(intersect, s);
                    //L = L.AddTo(B * Ld);
                    L.AddTo(B * Ld);
                }


                //importance sampling
                Vector3 wi;
                double pr;
                Spectrum f = Spectrum.ZeroSpectrum;
                bool isSpecular;

 
                (f, wi, pr, isSpecular) = intersectShape.BSDF.Sample_f(wo, intersect);

                //if (f.IsBlack() || pr == 0) break;

                //posodobi akumultor in sledi novemu zarku wi
                //B = B * f * Utils.AbsCosTheta(wi) / pr;
                B = B * f * Vector3.AbsDot(wi, intersect.Normal) / pr;
                r = intersect.SpawnRay(wi);

                //ruska ruleta, q je dolocen glede na B, vec odbojev bo - manj svetlobe bo - vecji mora biti q
                if (bounces > 3) {
                    double q = Math.Max(0, 1 - B.Max()); //termination probability
                    double ran = Samplers.ThreadSafeRandom.NextDouble();
                    if (ran < q)
                    {
                        break;
                    }
                    //else {
                    //    Console.WriteLine("q = " + q);
                    //    Console.WriteLine(" ran = " + ran);
                    //    Console.WriteLine("**********************");
                    //    Console.WriteLine("                         ");
                    //}
                    B = B / (1 - q);
                }
            }

            return L;
    }

  }
}
