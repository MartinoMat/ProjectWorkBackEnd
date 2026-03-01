## Back-End ProjectWork n.16 - MARTINO MATTEO 

Per la scrittura del Server è stato usato l'IDE Visual Studio 2026 e un progetto ASP.NET Core (linguaggio C#). <br>
Per il corretto funzionamento si consiglia per tale ragione l'utilizzo del suddetto <a href="https://visualstudio.microsoft.com/it/">Visual Studio</a>.


  <summary>Requisiti Server:</summary>
  <ol>
    <li>Database PostgreSQL
      <ul>
        <li>Installare <a href="https://www.enterprisedb.com/downloads/postgres-postgresql-downloads">PostgreSQL</a></li>
		  <li>Creare la struttura ed effettuare inserimento dei dati effettuare un restore rispettivamente di <code>CreazioneDB.sql</code> e <code>InserimentoDatiDB.sql</code> contenuti nella cartella DB della Repo
	<ol>
		<li>Se si utilizza un comando Bash:
	<ol>
		<li>creare il database <code>createdb nome_database</code></li>
		<li>creare il database <code>psql -U username -d database_name -f CreazioneDB.sql</code></li>
		<li>Attendere il completamento della creazione</li>
		<li>creare il database <code>psql -U username -d database_name -f InserimentoDatiDB.sql</code></li>
		</ol></li>		
	<li>Se si utilizza pgAdmin:
	<ol>
		<li>creare il database</li>
		<li>Tasto destro sul database appena creato e premere su Restore selezionando il file CreazioneDB.sql e impostando il Format=Plain</li>
		<li>Attendere il completamento della creazione</li>
		<li>Tasto destro sul database appena creato e premere su Restore selezionando il file InserimentoDatiDB.sql e impostando il Format=Plain</li>
		</ol></li></ol>
<p>
	É fornito un utente preimpostato (CF: <code>RSSMRA80A01L219M</code>, Password: <code>psw</code> ) con caricati dei referti a suo nome
</p>
</li>
      </ul>
        <li>Creazione di un file chiamato <code>.ENV</code> da posizionare nella cartella <code>ProjectWorkServer</code> così composto:
    <p><code>JWT_KEY=chiaveJWT°
 DB_HOST=localhost*
 DB_PORT=NumeroPorta*
 DB_NAME=NomeDatabase*
 DB_USER=NomeUtentePostgre*
 DB_PASS=PasswordPostgre*</code><br>
	° Chiave per JWT di almeno 32 byte quindi 32 char UTF-8.<br>
	* Dati scelti durante l'installazione di PostgreSQL</p>
        </li>
    </li>
        <li>Pacchetti NuGet
			<p>Verificare esistenza dei seguenti pacchetti nel progetto <code>ProjectWorkServer > Dipendenze > Pacchetti</code></p>
        <ul>
          <li><a href="https://www.nuget.org/packages/DotNetEnv/3.1.1">DotNetEnv</a></li>          
          <li><a href="https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/10.0.2">Microsoft.AspNetCore.Authentication.JwtBearer</a></li>
          <li><a href="https://www.nuget.org/packages/Microsoft.AspNetCore.OpenApi/10.0.2">Microsoft.AspNetCore.OpenApi</a></li>
          <li><a href="https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/10.0.2">Microsoft.EntityFrameworkCore</a></li>
          <li><a href="https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/10.0.2">Microsoft.EntityFrameworkCore.Tools</a></li>
          <li><a href="https://www.nuget.org/packages/Npgsql/10.0.1">Npgsql</a></li>
           <li><a href="https://www.nuget.org/packages/Npgsql.EntityFrameworkCore.PostgreSQL/10.0.0">Npgsql.EntityFrameworkCore.PostgreSQL</a></li>
            <li><a href="https://www.nuget.org/packages/Swashbuckle.AspNetCore.Swagger">Swashbuckle.AspNetCore.Swagger</a></li>
            <li><a href="https://www.nuget.org/packages/Swashbuckle.AspNetCore.SwaggerGen">Swashbuckle.AspNetCore.SwaggerGen</a></li>
            <li><a href="https://www.nuget.org/packages/Swashbuckle.AspNetCore.SwaggerUI">Swashbuckle.AspNetCore.SwaggerUI</a></li>
        </ul>
        </li>
    </li>
  </ul>
