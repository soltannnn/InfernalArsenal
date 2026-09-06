using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using ThoriumMod.Projectiles;
using ThoriumMod.Utilities;

namespace InfernalEclipseWeaponsDLC.Content.Projectiles.SummonerPro.SentryPro
{
	public class VeryVeryStrangeSkullPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 11;
		}
		public override void SetDefaults()
		{
			Projectile.width = 60;
			Projectile.height = 60;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.sentry = true;
			Projectile.timeLeft = 36000;
			Projectile.tileCollide = false;
			Projectile.netImportant = true;
		}
		public override void PostDraw(Color lightColor)
		{
			Projectile projectile = Projectile;
			Color color = lightColor * 0.25f * Projectile.Opacity;
			Texture2D texture = null;
			float scaleOffset = 0.1f + pulseAmount;
			ProjectileExtras.DrawLikeVanilla(projectile, color, texture, default, null, default, 0f, scaleOffset);
		}
		public override void AI()
		{
			Player player = Main.player[base.Projectile.owner];
			if (this.hoverDown)
			{
				this.hoverAmount++;
				if (this.hoverAmount % 15 == 0)
				{
					Projectile projectile = base.Projectile;
					projectile.position.Y = projectile.position.Y + 1f;
				}
				if (this.hoverAmount > 90)
				{
					this.hoverDown = false;
					this.hoverAmount = 0;
				}
			}
			else
			{
				this.hoverAmount++;
				if (this.hoverAmount % 15 == 0)
				{
					Projectile projectile2 = base.Projectile;
					projectile2.position.Y = projectile2.position.Y - 1f;
				}
				if (this.hoverAmount > 90)
				{
					this.hoverDown = true;
					this.hoverAmount = 0;
				}
			}
			if (!this.pulseShift)
			{
				this.pulseAmount += 0.0025f;
				if (this.pulseAmount >= 0.15f)
				{
					this.pulseShift = true;
				}
			}
			else if (this.pulseShift)
			{
				this.pulseAmount -= 0.0025f;
				if (this.pulseAmount <= 0f)
				{
					this.pulseShift = false;
				}
			}
			NPC npc = base.Projectile.FindNearestNPC(700f, false, false, null); //Problem here, for some reason thorium utities is not active here.
			if (npc != null)
			{
				Vector2 vector = npc.Center - base.Projectile.Center;
				base.Projectile.rotation = Utils.ToRotation(vector) - 1.57f;
				base.Projectile.ai[1] += 1f;
				if (base.Projectile.ai[1] == 60f)
				{
					base.Projectile.ai[0] = 1f;
				}
				if (base.Projectile.ai[1] >= 90f)
				{
					base.Projectile.ai[1] = 0f;
					if (Main.myPlayer == base.Projectile.owner)
					{
						IEntitySource source_FromThis = base.Projectile.GetSource_FromThis(null);
						float speed = 7.5f;
						float mag = vector.Length();
						if (mag > speed)
						{
							mag = speed / mag;
							vector *= mag;
						}
						Projectile.NewProjectile(source_FromThis, base.Projectile.Center, vector, ModContent.ProjectileType<VeryVeryStrangeSkullShotPro>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 0f, 0f);
						return;
					}
				}
			}
			else
			{
				base.Projectile.rotation = 0f;
				base.Projectile.ai[1] = 0f;
			}
		}
		public override void OnKill(int timeLeft)
		{
			for (int i = 0; i < 15; i++)
			{
				int DustID = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, Terraria.ID.DustID.GemDiamond, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 100, default(Color), 2f);
				Main.dust[DustID].noGravity = true;
			}
		}
		public override void PostAI()
		{
			if (Projectile.ai[0] > 0f)
			{
				Projectile.frameCounter++;
				if (Projectile.frameCounter > 3)
				{
					Projectile.frame++;
					Projectile.frameCounter = 0;
				}
				if (Projectile.frame > 9)
				{
					Projectile.frame = 0;
					Projectile.ai[0] = 0f;
					return;
				}
			}
			else
			{
				Projectile.frame = 0;
			}
		}
		public bool hoverDown = true;
		public int hoverAmount;
		public bool pulseShift;
		public float pulseAmount;
	}
}