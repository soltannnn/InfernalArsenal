using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Rarities;
using InfernalEclipseWeaponsDLC.Content.Projectiles.SummonerPro.SentryPro;
using CalamityMod.Items.Materials;
using ThoriumMod.Items.Donate;

namespace InfernalEclipseWeaponsDLC.Content.Items.Weapons.Summoner
{
	public class VeryVeryStrangeSkull : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 245;
			Item.DamageType = DamageClass.Summon;
			Item.sentry = true;
			Item.mana = 10;
			Item.width = 20;
			Item.height = 20;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.noMelee = true;
			Item.autoReuse = true;
			Item.knockBack = 6f;
		    Item.value = Item.sellPrice(0, 7, 50, 0);
			Item.rare = DonatorRarity.Get(2);
			Item.UseSound = new SoundStyle?(SoundID.Item44);
            Item.shoot = ModContent.ProjectileType<VeryVeryStrangeSkullPro>();
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectileDirect(source, Main.MouseWorld, Vector2.Zero, type, damage, knockback, player.whoAmI, 0f, 0f, 0f).originalDamage = Item.damage;
			player.UpdateMaxTurrets();
			return false;
		}
		public override void AddRecipes()
        {
        CreateRecipe()
			.AddIngredient<StrangeSkull>(1)
			.AddIngredient<Necroplasm>(14)
            .AddIngredient<NightmareFuel>(8)
            .AddTile(TileID.LunarCraftingStation)
               .Register();
        }
	}
}