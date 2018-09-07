using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EvolutionHelpers : System.Object {

    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
    {
        return source.OrderBy<T, int>((item) => rand.Next());
    }

    public static System.Random rand = new System.Random();

    public static int PopulationSize = 32;

    public static float MutationRate = 0.05f;

    public static int TournamentSize = 4;

    // returns the winners of the tournament.  this is a naive implementation of tournament selection.
    public static List<Individual> TournamentSelection(List<Individual> population) {

        if (population.Count < EvolutionHelpers.TournamentSize) {
            return new List<Individual>(); // this is an error
        }

        population = population.Randomize().ToList();

        List<Individual> contestants = population.GetRange(0, EvolutionHelpers.TournamentSize);

        contestants = contestants.OrderByDescending(i => i.fitness).ToList();

        List<Individual> winners = new List<Individual>();

        winners.Add(contestants[0]);
        winners.Add(contestants[1]);

        return winners;
    }
       
}
