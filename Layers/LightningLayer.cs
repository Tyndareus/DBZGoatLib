﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using DBZGoatLib.Handlers;

namespace DBZGoatLib.Layers
{
	public class LightningLayer : PlayerDrawLayer
	{
		public override Position GetDefaultPosition()
		{
			return new AfterParent(PlayerDrawLayers.Torso);
		}

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			return true;
		}

		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			if (Main.netMode != NetmodeID.Server)
			{
				if (drawInfo.shadow == 0f)
				{
					Player drawPlayer2 = drawInfo.drawPlayer;
					if (TransformationHandler.IsTransformed(drawInfo.drawPlayer))
					{
						var animData = TransformationHandler.GetCurrentTransformation(drawInfo.drawPlayer).Value.animationData;
						if(animData.Sparks) drawInfo.DrawDataCache.Add(LightningEffect(drawInfo.drawPlayer, drawInfo.Position, "DBZMODPORT/Dusts/LSSJ3Lightning"));
					}
				}
			}
		}

		public static DrawData LightningEffectDrawData(Player drawPlayer, Vector2 position, string lightningTexture)
		{
			int frame = drawPlayer.GetModPlayer<GPlayer>().lightningFrameTimer / 5;
			Texture2D texture = ModContent.Request<Texture2D>(lightningTexture, AssetRequestMode.AsyncLoad).Value;
			int frameSize = texture.Height / 3;
			int drawX = (int)(position.X + (float)drawPlayer.width / 2f - Main.screenPosition.X);
			int drawY = (int)(position.Y + (float)drawPlayer.height / 0.6f - Main.screenPosition.Y);
			return new DrawData(texture, new Vector2((float)drawX, (float)drawY), new Rectangle?(new Rectangle(0, frameSize * frame, texture.Width, frameSize)), Color.White, 0f, new Vector2((float)texture.Width / 2f, (float)texture.Height / 2f), 1f, 0, 0);
		}

		public static DrawData LightningEffect(Player drawPlayer, Vector2 position, string lightningTexture)
		{
			GPlayer modPlayer = drawPlayer.GetModPlayer<GPlayer>();
			Texture2D texture = ModContent.Request<Texture2D>(lightningTexture, AssetRequestMode.AsyncLoad).Value;
			int frameSize = texture.Height / modPlayer.lightning3FrameCount;
			int drawX = (int)(position.X + (float)drawPlayer.width / 2f - Main.screenPosition.X);
			int drawY = (int)(position.Y + (float)drawPlayer.height / 0.6f - Main.screenPosition.Y);
			return new DrawData(texture, new Vector2((float)drawX, (float)drawY), new Rectangle?(new Rectangle(0, frameSize * modPlayer.lightning3FrameIndex, texture.Width, frameSize)), Color.White, 0f, new Vector2((float)texture.Width / 2f, (float)frameSize + (float)drawPlayer.height / 2f), 1f, 0, 0);
		}
	}
}