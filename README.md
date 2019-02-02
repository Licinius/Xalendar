# Xalendar

Xalendar est une application développé avec le framework Xamarin, cette application permet de gérer les tâches à faire à la manière de [Coolendar](https://github.com/ToWaR6/Coolendar).

## Les fonctionnalités proposées

Xalendar propose les fonctionnalités suivantes : 

### Les fonctionnalités simples

* Enregistrer (Ajouter) un évènement à une date et heure précise avec un titre, une note et un type d'évènement parmi Anniversaire, Rendez-vous, Sport, Santé, Autres et Rien. Cette action est possible via le bouton "Add" en haut à droite de l'accueil. 
* Sur la page d'accueil, il est possible de chercher un événement en fonction du type de l'évènement ou de sa date.

### La base de données

* Xalendar sauvegarde les évènements dans une base de données SQLite. Un événément correspond au modèle suivant.
![Model Event](http://puu.sh/CG5oh/42eb87d141.png)

### Géolocalisation et prise de photo

* Lors de l'ajout d'un évènement l'utilisateur peut choisir d'enregistrer sa position, mais il peut aussi prendre une photo qui sera affichée lors de l'affichage détaillé de l'évènement.

### Notification en Xamarin

A l'aide de l'injection de dépendance l'utilisateur peut se voir afficher des notifications quand il est l'heure de réaliser son activité enregistrée auparavant. 
* Ainsi l'utilisateur peut choisir le type d'évènement qui verra des notifications affichées. 
* Les évènements de type "Sport" afficheront une activité particulière lors du clic.

### Utilisation de capteur en Xamarin

* Lors d'un clic sur une notification de type "Sport", une activité se lance avec un podomètre qui permettra à l'utilisateur de compter ses pas qui seront enregistrés sur l'évènement a posteriori.
