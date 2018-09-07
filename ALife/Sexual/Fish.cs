using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fish : Individual {

    public string name = "Fish";

    public static int GenomeLength = 5;
    public static int MaxGenomeValue = 10;
    public int[] genome;
    public List<int> Genome = new List<int>();

    public static int MaxResources = 30;
    public static int BaseResources = 5;
    public static int ResourcesToMate = 10;

    public Fish(int resources, int resourcesToMate, int maxResources) : base(resources, resourcesToMate, maxResources)
    {

        this.genome = new int[Fish.GenomeLength];

        for (int i = 0; i < this.genome.Length; i++)
        {
            this.genome[i] = EvolutionHelpers.rand.Next(0, Fish.MaxGenomeValue + 1);
        }

        this.Genome = this.genome.OfType<int>().ToList(); // slow, but since GenomeLength is only 5 it's fine.

        this.fitness = this.Genome.Sum(); 
    }

    public Fish() : this(Fish.BaseResources, Fish.ResourcesToMate, Fish.MaxResources)
    {

    }

    public Fish(string name) : this()
    {
        this.name = name;
    }

    // Version of Fish() where Genome is specified.  Used for reproduction.
    public Fish(int[] genome) : this()
    {
        this.genome = genome;
        this.Genome = this.genome.OfType<int>().ToList();
    }

    public override bool CanMate()
    {
        return this.resources >= this.resourcesToMate;
    }

    public override Individual Reproduce()
    {
        // WORKING AS INTENDED!  Do not try to asexually reproduce these Fish, they are sexual creatures
        throw new System.NotImplementedException();
    }

    public override Individual Reproduce(Individual other)
    {
        Fish o = other as Fish;

        int[] thisGenome = this.genome;
        int[] otherGenome = o.genome;
        int[] childGenome = new int[Fish.GenomeLength];

        // Uniform Crossover: each child gene is chosen independently, randomly from each parent
        double[] crossover = new double[Fish.GenomeLength];
        double[] mutations = new double[Fish.GenomeLength];

        // Populate crossover and mutation array with random numbers
        for (int i = 0; i < crossover.Length; i++) {
            crossover[i] = EvolutionHelpers.rand.NextDouble();
            mutations[i] = EvolutionHelpers.rand.NextDouble();
        }

        for (int j = 0; j < crossover.Length; j++) {

            if (crossover[j] > 0.5)
            {
                childGenome[j] = thisGenome[j];
            } else 
            {
                childGenome[j] = otherGenome[j];
            }

            if (mutations[j] < EvolutionHelpers.MutationRate) {
                childGenome[j] = Fish.MutateGene(childGenome[j]);
            }
        }

        Fish newFish = new Fish(childGenome);

        // recalculate fitness
        newFish.fitness = newFish.genome.Sum();

        return newFish;
    }

    public static int MutateGene(int orig)
    {
        if (EvolutionHelpers.rand.NextDouble() < EvolutionHelpers.MutationRate)
        {
            int ret = orig + (EvolutionHelpers.rand.Next(0, 3) - 1); // + {-1, 0, 1} (equal chance)
            return ret > Fish.MaxGenomeValue ? Fish.MaxGenomeValue : ret;
        }
        else
        {
            return orig;
        }
    }

    public override string ToString()
    {
        return this.name + " " + this.fitness.ToString();
    }
}
