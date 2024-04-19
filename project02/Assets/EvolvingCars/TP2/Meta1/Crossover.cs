using GeneticSharp.Domain.Chromosomes;
using System;
using System.Linq;
using UnityEngine;
using GeneticSharp.Domain.Randomizations;
using System.Collections.Generic;
using GeneticSharp.Domain.Crossovers;

namespace GeneticSharp.Runner.UnityApp.Commons
{
    public class Crossover : ICrossover
    {
        public int ParentsNumber { get; private set; }

        public int ChildrenNumber { get; private set; }

        public int MinChromosomeLength { get; private set; }

        public bool IsOrdered { get; private set; } // indicating whether the operator is ordered (if can keep the chromosome order).

        protected float crossoverProbability;
        
        // Crossover options
        public bool IsUniformCrossover {  get; private set; }
        public int KPoints {  get; private set; }



        public Crossover(float crossoverProbability) : this(2, 2, 2, true)
        {
            this.crossoverProbability = crossoverProbability;
        }

        public Crossover(int parentsNumber, int offSpringNumber, int minChromosomeLength, bool isOrdered)
        {
            ParentsNumber = parentsNumber;
            ChildrenNumber = offSpringNumber;
            MinChromosomeLength = minChromosomeLength;
            IsOrdered = isOrdered;
        }

        public IList<IChromosome> Cross(IList<IChromosome> parents)
        {
            /*
            IList<IChromosome> cross;
            if (IsUniformCrossover)
                cross = UniformCrossover(parents);
            else
                cross = KPointCrossover(parents);
            */

            return UniformCrossover(parents);
        }

        public IList<IChromosome> UniformCrossover(IList<IChromosome> parents)
        {
            IChromosome parent1 = parents[0];
            IChromosome parent2 = parents[1];
            IChromosome offspring1 = parent1.Clone();
            IChromosome offspring2 = parent2.Clone();

            for (int i = 0; i < parent1.Length; i++)
            {
                if (RandomizationProvider.Current.GetInt(0, 2) == 1)
                { 
                    offspring1.ReplaceGene(i, parent2.GetGene(i)); 
                    offspring2.ReplaceGene(i, parent1.GetGene(i));
                }
            }
            return new List<IChromosome> { offspring1, offspring2 };
        }

        public IList<IChromosome> KPointCrossover(IList<IChromosome> parents)
        {
            IChromosome parent1 = parents[0];
            IChromosome parent2 = parents[1]; 
            IChromosome offspring1 = parent1.Clone();
            IChromosome offspring2 = parent2.Clone();
            int nrPoints = 0;    






            for (int i = 1; i < parent1.Length-1; i++)
            {
                if (UnityEngine.Random.Range(0, 2) == 0)
                    continue;

                nrPoints++;
                 
                offspring1.ReplaceGenes(i, parent2.GetGenes().ToArray());                
            }


            
            offspring1.ReplaceGenes(0, parent2.GetGenes().ToArray());
            offspring2.ReplaceGenes(0, parent1.GetGenes().ToArray());

            return new List<IChromosome> { offspring1, offspring2 };
        }
    }
}