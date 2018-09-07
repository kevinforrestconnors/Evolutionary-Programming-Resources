using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class Individual {

    private int[] genome;
    public int fitness;

    public Individual() : this(false) { }

    public Individual(bool newIndividual)
    {
        if (newIndividual) {
            this.genome = new int[EvolutionHeuristics.GenomeLength];

            for (int i = 0; i < this.genome.Count(); i++)
            {
                this.genome[i] = EvolutionHeuristics.rand.Next(0, 1);
            }

            this.fitness = this.genome.Sum();
        }
    }

    public int MutateGene(int orig)
    { 
        if (EvolutionHeuristics.rand.NextDouble() < EvolutionHeuristics.MutationRate) {
            return orig == 1 ? 0 : 1;
        } else
        {
            return orig;
        }
    }

    public Individual Reproduce()
    {
        Individual ret = new Individual();

        ret.genome = this.genome;

        ret.genome = ret.genome.Select(i => this.MutateGene(i)).ToList().ToArray();

        ret.fitness = ret.genome.Sum();

        return ret;
    }

    public override string ToString()
    {
        return this.fitness.ToString();
    }
}
