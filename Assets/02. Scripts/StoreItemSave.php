<?php

header('Content-Type: application/json'); 

$hostname = '192.168.35.229';
$dbUsername = 'root';
$dbPassword = '1q2w3e4r!@#$';
$database = 'logingame';


try {
    $pdo = new PDO("mysql:host=$hostname;dbname=$database", $dbUsername, $dbPassword);
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

    
    $query = $pdo->prepare("SELECT item,itemMoney FROM items");
    $query->execute();
    $result = $query->fetchAll(PDO::FETCH_ASSOC);


    echo json_encode($result);

} catch (PDOException $e) {
}
?>
