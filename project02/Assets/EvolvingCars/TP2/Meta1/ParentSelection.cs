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

    public bool IsRouletteSelection;
    public int tournamentSize;

    public ParentSelection(int tournamentSize) : base(2)
    {
        this.tournamentSize = tournamentSize;
    }

    protected override IList<IChromosome> PerformSelectChromosomes(int number, Generation generation)
    {
        return TournamentSelection(number, generation); 
    }

    private IList<IChromosome> TournamentSelection(int number, Generation generation)
    {
        IList<CarChromosome> population = generation.Chromosomes.Cast<CarChromosome>().ToList();
        IList<IChromosome> parents = new List<IChromosome>();

        // While the required number of parents is not reached
        while (parents.Count < number)
        {
            // Get unique random indexes of individuals in the population
            int[] randomIndexes = RandomizationProvider.Current.GetUniqueInts(5, 0, population.Count);
            List<CarChromosome> selectedElements = new List<CarChromosome>();

            foreach (int index in randomIndexes)     
                // Add the element at the current index to the list
                selectedElements.Add(population[index]);
            

            // Sort the selected elements by fitness in descending order
            var sortedElements = selectedElements.OrderByDescending(element => element.Fitness).ToList();

            // Add the fittest individual to the parents list
            parents.Add(sortedElements[0]);
        }

        UnityEngine.Debug.Log("Tournament Selection done");
        return parents;
    }



    // TODO: Fix, not working
    private IList<IChromosome> RouletteSelection(int number, Generation generation)
    {
        IList<CarChromosome> population = generation.Chromosomes.Cast<CarChromosome>().ToList();
        IList<IChromosome> parents = new List<IChromosome>();
        float totalFitness = population.Sum(individual => individual.Fitness); 
        List<(float, CarChromosome)> roulette = new List<(float, CarChromosome)> { };
        float previousProbability = 0;
        // Create the roulette wheel
        foreach (var indivual in population)
        {
            previousProbability = previousProbability + (indivual.Fitness / totalFitness);
            roulette.Add((previousProbability, indivual));
        }

        // Spin the roulette wheel twice and select individuals
        while (parents.Count < number)
        {
            float spin = RandomizationProvider.Current.GetFloat(0, 1); // Generate a random number between 0 and 1
            CarChromosome selectedChromosome = null;

            // Find the individual where the spinner lands
            foreach (var (probability, chromosome) in roulette)
            {
                if (spin <= probability && !parents.Contains(chromosome))
                {
                    selectedChromosome = chromosome;
                    break;
                }
            }

            // If no individual is selected (due to rounding errors), select the last one
            if (selectedChromosome == null)
            {
                selectedChromosome = roulette.Last().Item2;
            }

            parents.Add(selectedChromosome);
        }

        return parents;
    }

}
