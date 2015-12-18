using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.Definitions;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Domain.DataAccessDefinitions;

namespace Liath.BigSpace.Implementations
{
	public class UpgradeManager : IUpgradeManager
	{
		private readonly IEnumRepository _enumRepository;

		public UpgradeManager(IEnumRepository enumRepository)
		{
			if (enumRepository == null) throw new ArgumentNullException(nameof(enumRepository));
			_enumRepository = enumRepository;
		}

		public void EnsureEnumValuesAreSynced()
		{
			this.SyncEnumValues<Commands>();			
		}

		private void SyncEnumValues<T>() where T: struct, IConvertible
		{			
			foreach (var enumValue in Enum.GetValues(typeof(T)).Cast<T>())
			{
				var value = Convert.ToInt32(enumValue);
				if (!_enumRepository.DoesEnumValueExist(typeof (T), value))
				{
					var code = Enum.GetName(typeof(T), enumValue);
					var humanName = this.GetHumanFriendlyName(enumValue) ?? code;
					_enumRepository.InsertEnumValue(typeof(T), value, code, humanName);
				}
			}
		}

		private string GetHumanFriendlyName<T>(T value)
		{
			MemberInfo memberInfo = typeof(T).GetMember(value.ToString()).FirstOrDefault();
			if (memberInfo == null) return null;

			HumanFriendlyNameAttribute attribute = (HumanFriendlyNameAttribute)memberInfo
				.GetCustomAttributes(typeof(HumanFriendlyNameAttribute), false)
				.FirstOrDefault();

			return attribute?.FriendlyValue;
		}

		//public GPUShaderAttribute GetGPUShader(EffectType effectType)
		//{
		//	MemberInfo memberInfo = typeof(EffectType).GetMember(effectType.ToString())
		//																						.FirstOrDefault();

		//	if (memberInfo != null)
		//	{
		//		GPUShaderAttribute attribute = (GPUShaderAttribute)
		//								 memberInfo.GetCustomAttributes(typeof(GPUShaderAttribute), false)
		//													 .FirstOrDefault();
		//		return attribute;
		//	}

		//	return null;
		//}
	}
}
