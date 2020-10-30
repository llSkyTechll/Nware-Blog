Le tout est développé avec Visual Studio 2019.

Pour tout les tests, j'ai utilisé WAMP pour Apache et MySQL.

Il sera peut-être nécessaire de changer la connexion string en fonction du port utilisé par le localhost. 
Le tout est dans la fonction sqlConnection dans AppStart/WebApiConfig.cs

Dans le dossier se trouve le fichier CreateMySQLDatabase.sql qui devrait créer la base de donnée ainsi que quelques enregistrement.

Pour la liaison entre Visual Studio et MySQL, j'ai suivi les étaps dans la vidéo suivante: https://www.youtube.com/watch?v=s1secsqSWLs

Pour ce qui est de l'API, il faut commencer par api/.
ex: localhost/api/categories/

Si cela ne fonctionne pas, vous pouvez me contacter par téléphone au 418-957-9527 ou par courriel: Alexandre.Reny98@gmail.com