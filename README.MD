![Logo](https://github.com/aqillakhamis/SESAK-Crowd-Simulation-Tool/blob/main/logo/sesak_logo.PNG)

--------------------------------------------------------------------------------------

# SESAK: A Crowd Simulation Tool
[![License: CC BY-NC-ND 4.0](https://img.shields.io/badge/License-CC%20BY--NC--ND%204.0-lightgrey.svg)](https://creativecommons.org/licenses/by-nc-nd/4.0/)

## Software Description
SESAK is a copyright crowd simulation software (copyright no: MYIPO LY2017001317) that has been developed by our team to simulate the movement of crowd in a space/area to provide insights on how crowd would behave in different environments. It can be used to evaluate performance and safety of environment in every phase of the life cycle; from design to operation. In this tool, SESAK software will be integrated with building design drawing to anticipate congestions occurences and propose alternative solutions or remedial actions to designers using swarm intelligence optimization. 

## Model Development 
* Crowd Dynamics using Social Force Model 
* Path Finding using A* Algorithm
* Optimization Engine using Artificial Bee Colony Optimization

## Environment Drawing
* Support Modular Type Building Design (using vertical and horizontal line)
* Customized Door Sizes
* Customized Comfort Test Zone
* Customized Evacuation Area
* Customized Agent Position, Walk Speed and Body Radius

## Fundamental Analysis
* Evacuation Time
* Number of Evacuated People
* Crowd Speed, Flow and Density (Fundamental Graph)

## Installation

### Environment Support 
Windows 10 

### Quick Setup
1. Clone this repo by run this command:  
```
git clone https://github.com/aqillakhamis/SESAK-Crowd-Simulation-Tool.git
```

2. Run executable application located inside __*bin/v1.0*__ folder

3. Run application __*Sesak.exe*__

4. Guideline on how to use this software can be referred from __*user manual*__ folder

## References
There are 2 type of objective function used in the optimization algorithm which are;
* single-objective problem (evacuation time)
* multi-objective problem (evacuation time and discomfort)

Detail publications on the crowd simulation model (crowd dynamics) and optimization algorithm used in this software is available from here. 

[1] Khamis, N., Selamat, H., Ismail, F. S., Lutfy, O. F., Haniff, M. F., & Nordin, I. N. A. M. (2020). Optimized exit door locations for a safer emergency evacuation using crowd evacuation model and artificial bee colony optimization. Chaos, Solitons & Fractals, 131, 109505.

```bibtex
@article{khamis2020optimized,
  title={Optimized exit door locations for a safer emergency evacuation using crowd evacuation model and artificial bee colony optimization},
  author={Khamis, Nurulaqilla and Selamat, Hazlina and Ismail, Fatimah Sham and Lutfy, Omar Farouq and Haniff, Mohamad Fadzli and Nordin, Ili Najaa Aimi Mohd},
  journal={Chaos, Solitons \& Fractals},
  volume={131},
  pages={109505},
  year={2020},
  publisher={Elsevier}
}
```

[2] Khamis, N., Selamat, H., Ismail, F. S., & Lutfy, O. F. (2019). Optimal exit configuration of factory layout for a safer emergency evacuation using crowd simulation model and Multi-Objective Artificial Bee Colony optimization. International Journal of Integrated Engineering, 11(4).

```bibtex
@article{khamis2019optimal,
  title={Optimal exit configuration of factory layout for a safer emergency evacuation using crowd simulation model and Multi-Objective Artificial Bee Colony optimization},
  author={Khamis, Nurulaqilla and Selamat, Hazlina and Ismail, Fatimah Sham and Lutfy, Omar Farouq},
  journal={International Journal of Integrated Engineering},
  volume={11},
  number={4},
  year={2019}
}
```

## Contact 
If you have any technical issues in using this software, please do not hesitate to contact us via this email aqillakhamis@gmail.com, hazlina@utm.my 

