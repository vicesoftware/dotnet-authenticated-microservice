CREATE TABLE hobbies(
   hobby_id serial PRIMARY KEY,
   hobby VARCHAR (50) UNIQUE NOT NULL
);

INSERT INTO hobbies(hobby) VALUES('jogging');
INSERT INTO hobbies(hobby) VALUES('hiking');
INSERT INTO hobbies(hobby) VALUES('swimming');
INSERT INTO hobbies(hobby) VALUES('diving');
INSERT INTO hobbies(hobby) VALUES('cooking');
INSERT INTO hobbies(hobby) VALUES('reading');