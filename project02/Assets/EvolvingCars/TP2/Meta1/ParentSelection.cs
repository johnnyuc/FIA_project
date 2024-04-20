using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Infrastructure.Framework.Texts;
using GeneticSharp.Runner.UnityApp.Car;
using UnityEngine;

public class ParentSelection : SelectionBase
{
    public ParentSelection() : base(2)
    {
    }

    protected override IList<IChromosome> PerformSelectChromosomes(int number, Generation generation)
    {

        IList<CarChromosome> population = generation.Chromosomes.Cast<CarChromosome>().ToList();
        IList<IChromosome> parents = new List<IChromosome>();
        
        /* YOUR CODE HERE */
        /*REPLACE THESE LINES BY YOUR PARENT SELECTION IMPLEMENTATION*/
        int[] randomIndexes = RandomizationProvider.Current.GetUniqueInts(number, 0, population.Count);
        while (parents.Count < number)
        {
            parents.Add(population[randomIndexes[parents.Count]]);
        }
        /*END OF YOUR CODE*/

        return parents;
    }

    private IList<IChromosome> RouletteSelection(int number, Generation generation)
    {
        IList<CarChromosome> population = generation.Chromosomes.Cast<CarChromosome>().ToList();
        IList<IChromosome> parents = new List<IChromosome>();
        float totalFitness = population.Sum(individual => individual.Fitness); 
        List<(float, CarChromosome)> roulette = new List<(float, CarChromosome)> { };
        foreach (var indivual in population)
        {
            roulette.Add((indivual.Fitness / totalFitness, indivual));
        }

        while(parents.Count < number)
        {
            float pick = RandomizationProvider.Current.GetFloat(0, 1);

        }

        int[] randomIndexes = RandomizationProvider.Current.GetUniqueInts(number, 0, population.Count);
        while (parents.Count < number)
        {
            parents.Add(population[randomIndexes[parents.Count]]);
        }

        return parents;
    }

}
