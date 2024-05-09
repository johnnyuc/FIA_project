using System;
using System.Diagnostics;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Runner.UnityApp.Car;

public class Mutation : IMutation
{
    public bool IsOrdered { get; private set; } // indicating whether the operator is ordered (if can keep the chromosome order).

    public Mutation()
    {
        IsOrdered = true;
    }

    public void Mutate(IChromosome chromosome, float probability)
    {
        // Iterate over each gene in the chromosome
        for (int i = 0; i < chromosome.Length; i++)
        {
            // Check if a mutation should occur for this gene
            if (RandomizationProvider.Current.GetDouble() < probability)
            {
                double geneValue = (double)chromosome.GetGene(i).Value;
                // Generate a noise value [-1, 1)
                double noise = (RandomizationProvider.Current.GetDouble()*2) - 1;

                // Mutate the gene - replace it with a new random value
                chromosome.ReplaceGene(i, new Gene(geneValue + noise));
                UnityEngine.Debug.Log("Mutated");
            }
        }
    }

}
