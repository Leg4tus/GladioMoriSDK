
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace MoveClasses
{
    

        public enum EquipmentPosition
        {
            HandLeft,
            HandRight,
            Helmet,
            ThighLeft,
            ThighRight,
            LegLeft,
            LegRight,
            Chest,
            BicepRight,
            BicepLeft,
            ArmRight,
            ArmLeft,
            Stomach,
            Hip
            
        }

        public enum EquipmentType
        {
            Spear,
            Shield,
            ShortSword,
            Rapier,
            BeardedAxe,
            SteelSlab,
            GreatHelmet,
            Cuisses,
            Greaves,
            Cuirass,
            Vambrace,
            Rerebrace,
            Plackart,
            Faulds,
            Bardiche,
            Dagger,
            Katana,
            Halberd,
            Katar,
            Zweihander,
            DaneAxe,
            Longsword,
            Gladius,
            Scutum,
            Claymore,
            NasalSpangenhelm,
            KettleHelmet,
            Twinblade,
            Kilij,
            Naginata,
            HookSword,
            Falx,
            Sica,
            Parma,
            Podao,
            ShortSpear,
            GouRang,
            Kanabo,
            Mace,
            BoStaff,
            JoStaff,
            Mallet,
            Goedendag,
            RomanHelmet,
            BattleAxe,
            Modded


        }


        [System.Serializable]
        public class EquipmentTypeItem
        {

            public List<EquipmentPosition> equipmentPositions;
            public int equipmentPoints = 5;
            public Texture2D icon;

        }


    
}