// ����� ������. �������� shotgun ����������, �� ���� ��� ����� ������ ������ (��������, ��������, ������� � �.�.)
// ---------------------------------------------------------------------------------------------------------------
// ���� ShotgunInst.ShootTo ����� Vector3(0.666f,0.666f,0.666f) - ������ �������� �����.
// ���� ShotgunInst.ShootTo ����� ������ �������� - �������� �������� � ������� ����� �������
// ---------------------------------------------------------------------------------------------------------------
// Particles ���� ���������� �������� ��������.
// � ShotgunInst ���� ���� ���� � ������ ��������� ��������, ������� ����� �������� � �������
// ---------------------------------------------------------------------------------------------------------------

using UnityEngine;

public sealed class Weapon : WeaponProperties
{
    #region Alterable values
    // ---------------------------------------------------------------------------------------------------------------
    [Header("Design & Visual parameters:")]
    public  ParticleSystem  particles;
    public  AudioClip[]     shootClips;
    // ---------------------------------------------------------------------------------------------------------------
    #endregion

    public      void    FixedUpdate     ()
    {
        if (currentReload > 0)
        {
            currentReload--;
        }
    }
    internal    void    TryShoot        (in Vector3 _targetPosition)
    {
        shootTargetPosition = _targetPosition;
        shooted = true;
        Shoot();
    }
    internal    void    Shoot           (in bool autoReloadSet = true)
    {
        if (shooted is false)
        {
            return;
        }

        shooted = false;

        if (shootTargetPosition == new Vector3(0.666f, 0.666f, 0.666f))
        {
            Shoot(bulletInstance, particles.transform.gameObject);
        }
        else
        {
            Shoot(bulletInstance, particles.transform.gameObject, shootTargetPosition);
        }

        if (particles != null)
        {
            particles.Emit(25);
        }
        if (shootClips.Length > 0)
        {
            SoundSpots.Generate(transform, shootClips[Random.Range(0, shootClips.Length)], out AudioSource aSource);
        }
    }
}

