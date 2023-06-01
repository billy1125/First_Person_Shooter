using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGunStatus
{
    int magazineSize { get; set; }
    bool isReloading { get; }
    int bulletsLeft { get; set; }
}

public interface IAttack
{
    void Attack();
}

public interface IReload
{
    void Reload();
}