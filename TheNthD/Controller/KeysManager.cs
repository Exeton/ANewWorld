using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_Nth_D.Controller
{
	class KeysManager
	{
		I2DMovementController movementController;
		Dictionary<Keys, IKeyModuel> keyBinds = new Dictionary<Keys, IKeyModuel>();

		public KeysManager(I2DMovementController movementController)
		{
			this.movementController = movementController;
		}

		public void registerKeybind(Keys key, IKeyModuel keyBind)
		{
			keyBinds.Add(key, keyBind);
		}

		public void onKeyDown(Keys key)
		{
			IKeyModuel moduel;
			if (keyBinds.TryGetValue(key, out moduel))
			{
				moduel.onKeyDown();
			}
		}

		public void onKeyUp(Keys key)
		{
			IKeyModuel moduel;
			if (keyBinds.TryGetValue(key, out moduel))
			{
				moduel.onKeyUp();
			}
		}

		//up down left right
		public bool[] keys = new bool[4];

		public void handelInput()
		{
			if (keys[0])
				movementController.handelUpInput();
			if (keys[1])
				movementController.handelDownInput();
			if (keys[2])
				movementController.handelLeftInput();
			if (keys[3])
				movementController.handelRightInput();
		}
	}
}
