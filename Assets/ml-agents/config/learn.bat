@echo off

set %1
set %2

@echo on

mlagents-learn --train --time-scale=100 --num-envs=3 --env=../../../Build/TowerDefense.exe --target-frame-rate=-1 --run-id=%RUNID% --quality-level=0 --curriculum=curricula/Suicide velocity_trainer_ppo_config.yaml %EXTRAARGS%