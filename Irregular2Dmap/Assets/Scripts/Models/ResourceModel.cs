using Assets.Scripts.Controllers;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class ResourceModel : IModel
    {
        public string Name { get; set; }
        public RarityEnum Rarity { get; set; }
        public DepositTypeEnum DepositType { get; set; }
        public float DepositAmount { get; set; }

        public ResourceModel(string name, RarityEnum rarityEnum, DepositTypeEnum depositTypeEnum = DepositTypeEnum.Tiny, float depositAmount = 0)
        {
            Name = name;
            Rarity = rarityEnum;
            DepositType = depositTypeEnum;
            DepositAmount = depositAmount;
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
            if (depositTypeThreshold > (int)DepositTypeEnum.Tiny && depositTypeThreshold <= (int)DepositTypeEnum.Small)
            {
                DepositType = DepositTypeEnum.Small;
            }
            else if (depositTypeThreshold > (int)DepositTypeEnum.Small && depositTypeThreshold <= (int)DepositTypeEnum.Average)
            {
                DepositType = DepositTypeEnum.Average;
            }
            else if (depositTypeThreshold > (int)DepositTypeEnum.Average && depositTypeThreshold <= (int)DepositTypeEnum.Large)
            {
                DepositType = DepositTypeEnum.Large;
            }
            else if (depositTypeThreshold > (int)DepositTypeEnum.Large)
            {
                DepositType = DepositTypeEnum.Abundant;
            }
            else
            {
                DepositType = DepositTypeEnum.Tiny;
            }
        }

        private void CalculateDepositAmount(BiomeModel biomeModel)
        {
            DepositAmount = (int)DepositType * biomeModel.Area * (float)(GameController.Instance.RNG.NextDouble() * (1.1 - 0.9) + 0.9) / (int)Rarity;
        }

        private bool Equals(ResourceModel resourceModel)
        {
            return Name.Equals(resourceModel.Name);
        }
    }
}
