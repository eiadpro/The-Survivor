// Copyright 2021, Infima Games. All Rights Reserved.

using System.Globalization;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Total Ammunition Text.
    /// </summary>
    public class TextAmmunitionTotal : ElementText
    {
        #region METHODS

        /// <summary>
        /// Tick.
        /// </summary>
        protected override void Tick()
        {
            //Total Ammunition.
            float ammunitionTotal = equippedWeapon.AmmunitionTotal();

            //Update Text.
            if(equippedWeapon.name == "P_LPSP_WEP_AR_01")
            textMesh.text = ammunitionTotal.ToString(CultureInfo.InvariantCulture);
            else
            textMesh.text = "∞";
        }
        
        #endregion
    }
}