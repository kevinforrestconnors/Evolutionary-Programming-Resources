using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class GA : MonoBehaviour {

    public List<Individual> pop;

    public GA()
    {
        this.pop = new List<Individual>();

        for (int i = 0; i < EvolutionHeuristics.PopulationSize; i++)
        {
            this.pop.Add(new Individual(true));
        }
    }

	// Use this for initialization
	void Awake () {

        this.pop = this.pop.OrderBy(o => o.fitness).ToList();

        Debug.Log(this.pop[0].fitness);
        Debug.Log(this.pop[this.pop.Count() - 1].fitness);

        for (int index = 0; index < 100000; index++)
        {

            if (this.pop[this.pop.Count() - 1].fitness == EvolutionHeuristics.GenomeLength)
            {
                break;
            }

            this.pop.RemoveRange(0, EvolutionHeuristics.PopulationSize / 2);

            List<Individual> children = this.pop.Select(i => i.Reproduce()).ToList();

            this.pop.AddRange(children);

            this.pop = this.pop.OrderBy(o => o.fitness).ToList();

            Debug.Log(this.pop[this.pop.Count() - 1].fitness);
        }

        foreach (Individual i in this.pop)
        {
            Debug.Log(i);
        }
    }

}
