-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Mar 08, 2018 at 11:12 PM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `epicreddit`
--
CREATE DATABASE IF NOT EXISTS `epicreddit` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `epicreddit`;

-- --------------------------------------------------------

--
-- Table structure for table `comments`
--

CREATE TABLE `comments` (
  `id` int(11) NOT NULL,
  `body` text NOT NULL,
  `user_id` int(11) NOT NULL,
  `post_id` int(11) NOT NULL,
  `parent_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `posts`
--

CREATE TABLE `posts` (
  `id` int(11) NOT NULL,
  `title` varchar(255) NOT NULL,
  `body` text NOT NULL,
  `user_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `posts`
--

INSERT INTO `posts` (`id`, `title`, `body`, `user_id`) VALUES
(9, ' Rattlesnake Ledge (Snoqualmie Pass)', 'Start: I-90 Exit 32 (map)\r\nDifficulty: Easy\r\nLength: 4 miles roundtrip\r\n\r\nFor a short and relatively easy day hike, it doesn’t get much better than a gently-sloping trail through old growth forest, a view over Snoqualmie Pass and the Southern Cascades, and a trailhead only 40 minutes east of Seattle. The hike starts off on the shores of Rattlesnake Lake and cuts through a series of switchbacks that rise 1,000 feet over 1.5 miles. The path is steadily uphill but never overly difficult, which means that the ledge is crowded on weekends and sunny summer days (it’s a great option for watching the sunset after work or as a trail run). Before or after the hike, the lake itself is a great place to relax, walk, swim, and fish. For tougher hikes near the city, popular Little Si and Mount Si (below) across the valley are similar but slightly more challenging.', 5),
(11, 'Little Si and Mount Si (Snoqualmie Pass)', 'Start: I-90 Exit 32 (map)\r\nDifficulty: Easy; moderate\r\nLength: 5 miles; 8 miles\r\n\r\nThe easy access to Little Si and Mount Si make them two of the highly-trafficked day hikes in the Seattle area. We like Little Si best, which is a 5-mile roundtrip excursion with around 1,200 feet in elevation gain. The forested path is excellent and there are a number of interesting rock formations along the way (it’s also very popular for climbing). The top has good views of North Bend, the Upper Snoqualmie Valley, and Mount Si hovering above to the north. Mount Si is a more involved day hike at a distance of 8 miles roundtrip and with an elevation gain of around 3,150 feet. It’s not that Mount Si isn’t a really nice hike, it’s just that we think there are better options for that kind of effort (see Granite Mountain below). But you can’t knock the proximity to Seattle—both Little Si and Mount Si are accessed via exit 32 on I-90, less than a 45-minute drive from downtown.', 5),
(12, 'Mt. Pilchuck (Mountain Loop Highway)', 'For a moderate one-day summit from Seattle, it’s tough to find a better hike than the one up Mt. Pilchuck. Located on the Mountain Loop Highway just over an hour north of downtown, this hike offers big-time scenery quickly and will not disappoint. You rise with views of the North Cascades and classic alpine terrain, finally curling around the summit for huge expanses including the Olympic Peninsula, Puget Sound, San Juan Islands, and Mt. Rainier. This hike is only 5.4 miles roundtrip but relatively steep and therefore intermediate in difficulty.', 5),
(13, 'Ebey’s Landing (Whidbey Island)', 'Start: End of Cook Road to the Prairie Overlook trailhead (map)\r\nDifficulty: Moderate\r\nLength: Up to 5.6 miles roundtrip\r\n\r\nPerched on the western shore of Whidbey Island with views of the Olympic Mountains and Strait of San Juan de Fuca, Ebey’s Landing is our favorite Washington coastal hike. Named after Issac Ebey who built the island’s first settlement, this trail passes high along the bluff with excellent opportunities for wildlife watching, including bald eagles soaring along the cliffs, seals and sea lions playing in the surf, and even orca pods feeding on salmon in the summer. Toward the end of the bluff, the trail intersects the original and restored homestead from 1850 before zigzagging down the side of the cliff to the stony beach (an easier option is to return the same way via the high side of the bluff). This is a superb early morning and sunset hike, although it’s excellent anytime the views are clear.', 5),
(14, 'Summerland (Mt. Rainier National Park)', 'Start: Mt. Rainier National Park—White River Entrance (map)\r\nDifficulty: Moderate\r\nLength: 8.4 miles roundtrip\r\n\r\nIf you can only do one summer full-day hike in Washington, the hike to Summerland in Mt. Rainier National Park is your best bet. This pristine alpine meadow is the starting point for climbers heading up Mt. Rainier’s southern slopes and one of the best places in the state to view wildflowers. The trail passes upward through old-growth forests along the White River until the trees give way to spectacular mountain vistas. Summerland, which is 4.2 miles from the trailhead, is an open meadow full of marmots, wildflowers, and spectacular views of Mt. Rainier. For those looking for more mileage, Panhandle Gap is another 1.4 miles from Summerland. This is a popular trail and it’s best to get started early before the bulk of the crowds arrive. Camping is possible in Summerland but spaces are limited and drawn via lottery months in advance. ', 5),
(15, ' Chain Lakes Loop (North Cascades/Mt. Baker)', 'Start: End of the Mt. Baker Highway (map)\r\nDifficulty: Moderate\r\nLength: 8 miles\r\n\r\nThe Chain Lakes Loop offers a grand tour of the North Cascades: towering peaks, wildflower meadows, shimmering lakes, and great views of Mt. Baker and Shuksan. From the Bagley Lakes Trailhead, the path leads past a series of lakes—some still bordered by snow late into the year—and up the Herman Saddle. The trail then navigates the pass between Table Mountain and Ptarmigan Ridge before continuing to Artist Point. Famous for the imagery of Mt. Shuksan reflecting in its waters, Artist Point is one of the highlights of hiking in the North Cascades. For those who don\'t have time for the entire loop, it’s also possible to park at the Artist Point lot and go directly to the lake (when the road is open). The Chain Lakes Loop is crowded on summer weekends but much less so during the week.', 5),
(16, 'Dungeness Spit (Olympic Peninsula)', 'Start: Dungeness Recreation Area (map)\r\nDifficulty: Moderate\r\nLength: Up to 11 miles roundtrip\r\n\r\nDungeness Spit is the longest coastal spit in the United States and one of our favorite coastal hikes in Washington. The narrow 5.5-mile long Spit, all of which is designated as the Dungeness National Wildlife Refuge, juts out from the northern coast of the Olympic Peninsula into the Strait of Juan de Fuca. At the end of the journey is an historic lighthouse and views of the Cascades and San Juan Islands. The area is great for bird watching and spotting sea life: bald eagles often fly overhead and sea lions and seals come ashore to rest amidst the driftwood. Although the Spit is flat, it is rocky and the walking can take longer than it would on a normal dirt trail. And make sure to bring proper sun protection as there are very few places to find shade.', 5),
(17, 'Kendall Katwalk (Snoqualmie Pass)', 'Start: I-90 Exit 52 (map)\r\nLength: 11 miles roundtrip\r\nDifficulty: Moderate to challenging\r\n\r\nThe Kendall Katwalk in the Cascades is an impossibly-thin ridge that makes up a spectacular section of the Pacific Crest Trail. Located off I-90 via Exit 52, the Katwalk is considered to be one of the most scenic mountain areas in Washington. The hike begins in an old growth forest, and as the elevation increases, the tree line falls away with great views of the Cascades and Mt. Rainier. After crossing a series of meadows and alpine rock fields, the trail climbs to the top of the ridge. The Kendall Katwalk itself is approximately 5.5 miles from the trailhead (11 miles roundtrip) and you can continue on to hike along a series of beautiful alpine lakes. This area also makes for a great overnight backpacking trip. \r\n\r\n', 5),
(18, 'Maple Pass Loop (North Cascades)', 'Start: Rainy Pass trailhead on North Cascades Highway (map)\r\nLength: 7.2 miles\r\nDifficulty: Moderate\r\n\r\nPerhaps the most stunning day hike in the North Cascades, and even all of Washington, is the Maple Pass Loop off the North Cascades Highway. We should start by mentioning that with driving, this hike makes for a very long day. It’s approximately three hours (of incredibly beautiful scenery) to the trailhead, and the hike itself is another 7.2 miles. But this is all attainable in a day and we think it’s well worth it. If you’re up for more of an adventure, you can camp in one of the public campgrounds in the Methow Valley before or after, which makes for a great one-night trip from Seattle.', 5),
(19, 'The Enchantments Traverse (Central Cascades)', 'Start: Icicle Creek Road outside Leavenworth (map)\r\nDifficulty: Challenging\r\nLength: 18 miles one-way\r\n\r\nThe Enchantments outside Leavenworth are one of Washington’s great natural treasures and it’s nearly impossible to overstate their beauty. Granite buttresses, glacier-rimmed lakes, alpine meadows, wildflowers, and mountain goats all make this one of the best hiking areas in the Northwest. Permits to camp here are awarded by a highly-contested lottery that takes place in the spring, with a limited number of permits handed out at the ranger station each morning from May through September (also via lottery). ', 5);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `username` varchar(30) NOT NULL,
  `password` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `username`, `password`) VALUES
(1, 'Mir', 'alskdjf'),
(2, 'why', 'really though why'),
(3, 'dog_lover_6', 'dog'),
(4, 'Angelo', 'myPassword'),
(5, 'qianqian', 'qianqian'),
(6, 'Ainur', 'ainur');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `comments`
--
ALTER TABLE `comments`
  ADD PRIMARY KEY (`id`),
  ADD KEY `post_id` (`post_id`),
  ADD KEY `parent_id` (`parent_id`),
  ADD KEY `comments_ibfk_3` (`user_id`);

--
-- Indexes for table `posts`
--
ALTER TABLE `posts`
  ADD PRIMARY KEY (`id`),
  ADD KEY `posts_ibfk_1` (`user_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `comments`
--
ALTER TABLE `comments`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `posts`
--
ALTER TABLE `posts`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;
--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `comments`
--
ALTER TABLE `comments`
  ADD CONSTRAINT `comments_ibfk_1` FOREIGN KEY (`post_id`) REFERENCES `posts` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `comments_ibfk_2` FOREIGN KEY (`parent_id`) REFERENCES `comments` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `comments_ibfk_3` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `posts`
--
ALTER TABLE `posts`
  ADD CONSTRAINT `posts_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
