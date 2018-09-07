using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class ALife : MonoBehaviour {

    public List<Individual> pop;

    public ALife()
    {
        this.pop = new List<Individual>();

        for (int i = 0; i < EvolutionHeuristics.PopulationSize; i++)
        {
            this.pop.Add(new Fish());
        }
    }

	// Use this for initialization
	void Awake () {

        this.pop = this.pop.OrderBy(o => o.fitness).ToList();

        Debug.Log(this.pop[0].fitness);
        Debug.Log(this.pop[this.pop.Count() - 1].fitness);

        for (int index = 0; index < 20000; index++)
        {

            List<Fish> toReproduce = new List<Fish>();

            foreach (Fish f in this.pop)
            {
                f.resources += 1;

                if (f.CanMate())
                {
                    toReproduce.Add(f);
                }
            }

            foreach (Fish f in toReproduce)
            {
                this.pop.Add(f.Reproduce());
                this.pop.RemoveAt(EvolutionHeuristics.rand.Next(this.pop.Count()));
            }
            
            this.pop = this.pop.OrderBy(o => o.fitness).ToList();

            Debug.Log(this.pop[this.pop.Count() - 1].fitness);
        }

        foreach (Individual i in this.pop)
        {
            Debug.Log(i);
        }
    }

}
