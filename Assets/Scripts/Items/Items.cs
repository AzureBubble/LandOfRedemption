using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items {

    //道具抽象类
    public abstract class Item
    {
        //道具名称
        protected string name;
        //道具持有者
        protected GameObject holder;
        //道具调用方法
        public abstract void ItemInvoke();

        public virtual void SetName(string name)
        {
            this.name = name;  
        }

        public virtual string GetName()
        {
            return this.name;
        }

        public virtual void SetHolder(GameObject obj)
        {
            this.holder = obj;
        }

        public static bool IsHolder(GameObject obj)
        {
            return (obj.GetComponent("ItemHolder")) ? true : false;
        }

        public static bool IsItem(GameObject obj)
        {
            int num = obj.GetComponents<ISceneItem>().Length;
            return (num > 0) ? true : false;
        }
        
    }

    interface ISceneItem
    {
        public void ItemInvoke();
    }
}
