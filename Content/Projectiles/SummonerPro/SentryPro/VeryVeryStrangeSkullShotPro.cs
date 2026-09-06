using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Sounds;
using CalamityMod.Buffs.DamageOverTime;

namespace InfernalEclipseWeaponsDLC.Content.Projectiles.SummonerPro.SentryPro
{
	public class VeryVeryStrangeSkullShotPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
			ProjectileID.Sets.SentryShot[Projectile.type] = true;
		}
		public override void SetDefaults()
		{
			Projectile.DamageType = DamageClass.Summon;
			Projectile.width = 6;
			Projectile.height = 6;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 180;
			Projectile.extraUpdates = 4;
		    Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 12;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			if (Projectile.timeLeft < 170)
			{
				Vector2 drawOrigin;
				drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
				for (int i = 0; i < Projectile.oldPos.Length; i++)
				{
					Vector2 drawPos = Projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
					Color color = Projectile.GetAlpha(new Color(255, 255, 255, 0) * 0.1f) * ((Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
					Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, 0, 0f);
				}
			}
			return true;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			if (Projectile.timeLeft < 170)
			{
				return new Color?(new Color(255, 255, 255, 0) * 0.75f);
			}
			return new Color?(new Color(255, 255, 255, 0) * 0f);
		}
		public override void AI()
		{
			if (Projectile.localAI[0] == 0f)
			{
				Projectile.localAI[0] = 1f;
				SoundEngine.PlaySound(ThoriumSounds.GasterBlast, Projectile.Center);
			}
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<Nightwither>(), 300, false);
		}
		public override void OnKill(int timeLeft)
		{
			for (int u = 0; u < 10; u++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemAmethyst, Utils.NextFloat(Main.rand, -1f, 1f), Utils.NextFloat(Main.rand, -1f, 1f), 0, default(Color), 1f);
				Main.dust[dust].noGravity = true;
			}
		}
	}
}