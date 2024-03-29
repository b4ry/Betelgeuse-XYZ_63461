﻿using Assets.Scripts.Controllers;
using Assets.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class ResourceModel : IFoundableObjectModel
    {
        private readonly Dictionary<RarityEnum, int> rarityOccurenceThresholds = new Dictionary<RarityEnum, int>()
        {
            { RarityEnum.Common, 0 },
            { RarityEnum.Uncommon, 20 },
            { RarityEnum.Rare, 40 },
            { RarityEnum.Legendary, 60 }
        };

        public string Name { get; set; }
        public RarityEnum Rarity { get; set; }
        public ResourceDepositSizeEnum DepositType { get; set; }
        public float DepositAmount { get; set; }
        public bool IsAvailable { get; set; }

        public ResourceModel(string name, RarityEnum rarityEnum, ResourceDepositSizeEnum depositTypeEnum = ResourceDepositSizeEnum.Tiny, float depositAmount = 0)
        {
            Name = name;
            Rarity = rarityEnum;
            DepositType = depositTypeEnum;
            DepositAmount = depositAmount;
            IsAvailable = false;
        }

        public override bool Equals(object obj)
        {
            var resourceModel = obj as ResourceModel;

            return Equals(resourceModel);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Rarity.GetHashCode();
        }

        public void DetermineAvailability(BiomeModel biomeModel, RegionModel regionModel)
        {
            var occurenceRating = biomeModel.Area * 2 * (int)biomeModel.Rarity * (GameController.Instance.Rng.NextDouble() + 0.2) * regionModel.Oddity.Rating / (int)Rarity;
            IsAvailable = occurenceRating >= rarityOccurenceThresholds[Rarity];
        }

        public void DetermineDeposits(RegionModel regionModel, BiomeModel biomeModel)
        {
            var depositTypeThreshold = biomeModel.Area * (int)biomeModel.Rarity * regionModel.Oddity.Rating / (int)Rarity;

            DetermineDepositType(depositTypeThreshold);
            CalculateDepositAmount(biomeModel);

            Debug.Log($"{depositTypeThreshold}");
            Debug.Log($"{regionModel.Name} - {Name}: {DepositType} - {DepositAmount}");
        }

        public void MergeDeposits(ResourceModel resourceModel)
        {
            DepositAmount += resourceModel.DepositAmount;

            var updatedDepositTypeThreshold = (int)DepositType + (int)resourceModel.DepositType;

            DetermineDepositType(updatedDepositTypeThreshold);
        }

        private void DetermineDepositType(float depositTypeThreshold)
        {
            if (depositTypeThreshold > (int)ResourceDepositSizeEnum.Tiny && depositTypeThreshold <= (int)ResourceDepositSizeEnum.Small)
            {
                DepositType = ResourceDepositSizeEnum.Small;
            }
            else if (depositTypeThreshold > (int)ResourceDepositSizeEnum.Small && depositTypeThreshold <= (int)ResourceDepositSizeEnum.Average)
            {
                DepositType = ResourceDepositSizeEnum.Average;
            }
            else if (depositTypeThreshold > (int)ResourceDepositSizeEnum.Average && depositTypeThreshold <= (int)ResourceDepositSizeEnum.Large)
            {
                DepositType = ResourceDepositSizeEnum.Large;
            }
            else if (depositTypeThreshold > (int)ResourceDepositSizeEnum.Large)
            {
                DepositType = ResourceDepositSizeEnum.Abundant;
            }
            else
            {
                DepositType = ResourceDepositSizeEnum.Tiny;
            }
        }

        private void CalculateDepositAmount(BiomeModel biomeModel)
        {
            DepositAmount = (int)DepositType * biomeModel.Area * (float)(GameController.Instance.Rng.NextDouble() * (1.1 - 0.9) + 0.9) / (int)Rarity;
            DepositAmount /= 10;
        }

        private bool Equals(ResourceModel resourceModel)
        {
            return Name.Equals(resourceModel.Name);
        }
    }
}
