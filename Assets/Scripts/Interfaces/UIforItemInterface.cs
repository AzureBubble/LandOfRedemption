using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IUIforItemInterface
    {
        //与黑板互动时显示密码UI
        public void ShowBlackBoardContent();

        //道具移除时的UI展示，name：道具名称，type：道具种类
        public void RemoveItem(string name, string type);

        //道具加入时的UI展示，name：道具名称，type：道具种类
        public void AddItem(string name, string type);

        //向左切换道具时的UI展示，leftItemName:道具栏左移后的道具名供校准
        public void SwitchItemLeft(string leftItemName);

        //向右切换道具时的UI展示，rightItemName:道具栏左移后的道具名供校准
        public void SwitchItemRight(string rightItemName);
    }
}
