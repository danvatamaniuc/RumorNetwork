# RumorNetwork
A small project that applies Evolutive Algorithms and Particle Swarmin Optimisation in order to find the minimal set of connections between the members of a group so that everyone can hear a rumor.

For the EA part, a steady-state algorithm was used to select the chromosomes using Tournament Selection, splicing them and mutate the offsprings.

The PSO part is similar, only is selecting the best particle at a given time, changig all the other's particles speed, which in turn dictates their movement. In this case, the mutation rate.
