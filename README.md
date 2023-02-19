# Path Tracer

Homework assignment for the Advanced Computer Graphics course at the University of Ljubljana, Faculty of Computer and Information Science. It aimed to put into practice some of the theoretical knowledge we learned about physically based rendering.

A path tracer framework written in C# was provided to us in order to implement the following missing parts:

* (*PathTracer.cs*) the main path tracing function itself that follows the path of the ray as it is sampled from the image plane and bounces throughout the scene until it hits a light source. Next-Event-Estimation and Russian roulette techniques are also implemented. 

* three BRDF models which control the material of an object with importance-sampling according to their distribution (*Samplers.cs*):
  * (*Lambertian.cs*) Lambertian model
  * (*PhongBlinn.cs*) Blinn-Phong model
  * (*SpecularReflection.cs*) specular reflection model

The GIF below demonstrates the rendering process of a simple scene with a dimension of 160x160 pixels. Walls' model is Lambertian, the front ball has a specular reflection model and the green ball in the back has the Blinn-Phong model. This first iteration traces 25600 paths. As more paths are traced the noise is reduced and the scene becomes more realistic.
* *Note that the framework is capable of parallel rendering but we turned it off for this demonstration.*

![demo](demo.gif)
