using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fish : Individual {

    public string name = "Fish";

    private static int GenomeLength = 5;
    private static int MaxGenomeValue = 10;
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
            this.genome[i] = EvolutionHeuristics.rand.Next(0, Fish.MaxGenomeValue + 1);
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

    // Version of Fish() where Genome is specified.  Used for cloning fish for reproduction.
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
        Fish newFish = new Fish(this.genome);

        newFish.Genome = newFish.Genome.Select(i => this.MutateGene(i)).ToList();
        newFish.genome = newFish.Genome.ToArray();

        // recalculate fitness
        newFish.fitness = newFish.genome.Sum();

        return newFish;
    }

    public override int MutateGene(int orig)
    {
        if (EvolutionHeuristics.rand.NextDouble() < EvolutionHeuristics.MutationRate)
        {
            int ret = orig + (EvolutionHeuristics.rand.Next(0, 3) - 1); // + {-1, 0, 1} (equal chance)
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
