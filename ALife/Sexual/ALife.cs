using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class ALife : MonoBehaviour {

    public List<Individual> pop;

    public ALife()
    {
        this.pop = new List<Individual>();

        for (int i = 0; i < EvolutionHelpers.PopulationSize; i++)
        {
            this.pop.Add(new Fish("Fish"));
        }
    }

	// Use this for initialization
	void Awake () {

        this.pop = this.pop.OrderByDescending(o => o.fitness).ToList();

        int generation = 0;

        while (this.pop[0].fitness != Fish.GenomeLength * Fish.MaxGenomeValue) // search for max value
        {

            Debug.Log("Generation: " + generation.ToString());
            generation++;

            List<Individual> toReproduce = new List<Individual>();

            foreach (Fish f in this.pop)
            {
                f.resources += 1;

                if (f.CanMate())
                {
                    toReproduce.Add(f);
                    f.resources = 0;
                }
            }

            // sort by fitness
            toReproduce = toReproduce.OrderByDescending(f => f.fitness).ToList();

            if (toReproduce.Count >= EvolutionHelpers.TournamentSize) {
                
                foreach (Fish f in toReproduce)
                {
                    List<Individual> parents = EvolutionHelpers.TournamentSelection(toReproduce);

                    this.pop.Add(parents[0].Reproduce(parents[1]));
                    this.pop.RemoveAt(EvolutionHelpers.rand.Next(this.pop.Count()));
                }
            }

            this.pop = this.pop.OrderByDescending(o => o.fitness).ToList();

            Debug.Log("Best: " + this.pop[0].ToString());

        }

        foreach (Individual i in this.pop)
        {
            Debug.Log(i);
        }

    }

}
